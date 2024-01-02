using Finder.Models;
using System.Text.Json;

namespace Finder.Repository
{
    public class WordFinder : IWordFinder
    {
        private static readonly Random random = new Random();
        private static AlphabetSoupResultDto soup = new AlphabetSoupResultDto();

        public AlphabetSoupResultDto GetMatrix() => soup;

        public AlphabetSoupResultDto GenerateMatrix(int matrixLength)
        {
            List<string> validWords;
            string contenidoJson = File.ReadAllText("valid_words.json");
            validWords = JsonSerializer.Deserialize<List<string>>(contenidoJson);

            char[,] matrix = new char[matrixLength, matrixLength];

            // Fill the array with random letters
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = ' ';
                }
            }

            // Select words that fit in the matrix
            List<WordsDto> selectedWords = validWords.Where(w => w.Length <= matrixLength)
                                                   .Select(w => new WordsDto
                                                   {
                                                       Name = w.ToUpper(),
                                                       Selected = false,
                                                       Found = true
                                                   })
                                                   .Take(matrixLength)
                                                   .ToList();

            foreach (var word in selectedWords)
            {
                // Generate a random direction for the word
                int direction = new Random().Next(0, 2);

                // Place the word in the corresponding address
                if (direction == 0)
                {
                    Horizontal(word.Name, matrix);
                }
                else
                {
                    Vertical(word.Name, matrix);
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == ' ')
                    {
                        matrix[i, j] = (char)('A' + random.Next(0, 26));
                    }
                }
            }

            // Convert the array to a list for easier manipulation
            List<List<LetterDto>> gridList = new List<List<LetterDto>>();

            for (int i = 0; i < matrixLength; i++)
            {
                List<LetterDto> row = new List<LetterDto>();
                for (int j = 0; j < matrixLength; j++)
                {
                    row.Add(new LetterDto
                    {
                        RowIndex = i,
                        ColIndex = j,
                        Name = matrix[i, j],
                        Selected = false
                    });
                }
                gridList.Add(row);
            }

            soup.Words = selectedWords;
            soup.Matrix = gridList;

            return soup;
        }

        public bool FindAndMatch(List<LetterDto> word)
        {
            string wordStr = string.Join("", word.Select(x => x.Name));
            soup.Words.Find(x => x.Name == wordStr).Selected = true;
            foreach (var row in soup.Matrix)
            {
                foreach (var letter in row)
                {
                    // Find if the letter is in the letter list
                    var found = word.FirstOrDefault(l => l.RowIndex == letter.RowIndex && l.ColIndex == letter.ColIndex);
                    if (found != null)
                    {
                        // Mark the letter as selected
                        letter.Selected = true;
                    }
                }
            }

            return true;
        }

        public bool FindAndSelectWord(string wordToFind)
        {
            if(soup.Words.Any(x => x.Name == wordToFind && !x.Selected))
            {
                var word = soup.Words.FirstOrDefault(x => x.Name == wordToFind);
                word.Selected = true;
                word.Found = MarkLettersAsSelected(wordToFind, soup.Matrix);
                return word.Found;
            }

            return false; 
        }

        private void Horizontal(string word, char[,] matrix)
        {
            // Generate a random position for the first letter of the word
            int posicionX = new Random().Next(0, matrix.GetLength(0));
            int posicionY = new Random().Next(0, matrix.GetLength(1) - word.Length + 1);

            // Place the first letter of the word on the grid
            matrix[posicionX, posicionY] = word[0];

            // Place the rest of the letters of the word on the grid
            for (int i = 1; i < word.Length; i++)
            {
                matrix[posicionX, posicionY + i] = word[i];
            }
        }

        private void Vertical(string word, char[,] matrix)
        {
            // Generate a random position for the first letter of the word
            int posicionX = new Random().Next(0, matrix.GetLength(0) - word.Length + 1);
            int posicionY = new Random().Next(0, matrix.GetLength(1));

            // Place the first letter of the word on the grid
            matrix[posicionX, posicionY] = word[0];

            // Place the rest of the letters of the word on the grid
            for (int i = 1; i < word.Length; i++)
            {
                matrix[posicionX + i, posicionY] = word[i];
            }
        }

        private bool MarkLettersAsSelected(string wordToFind, List<List<LetterDto>> matrix)
        {
            int wordIndex = 0;

            // We go through the matrix to find the word
            foreach (var row in matrix)
            {
                foreach (var letter in row)
                {
                    // Check if the current letter matches the first letter of the word
                    if (char.ToUpper(letter.Name) == wordToFind[wordIndex])
                    {
                        // Save the initial position of the match
                        int startRow = letter.RowIndex;
                        int startCol = letter.ColIndex;

                        // Check if the word matches horizontally to the right
                        if (CheckHorizontal(wordToFind, matrix, startRow, startCol))
                        {
                            SelectWordHorizontal(wordToFind, matrix, startRow, startCol);
                            return true;
                        }

                        // Check if the word matches vertically downwards
                        if (CheckVertical(wordToFind, matrix, startRow, startCol))
                        {
                            SelectWordVertical(wordToFind, matrix, startRow, startCol);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckHorizontal(string wordToFind, List<List<LetterDto>> matrix, int row, int col)
        {
            if (col + wordToFind.Length > matrix.Count)
            {
                return false;
            }

            for (int i = 0; i < wordToFind.Length; i++)
            {
                if (char.ToUpper(matrix[row][col + i].Name) != wordToFind[i])
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckVertical(string wordToFind, List<List<LetterDto>> matrix, int row, int col)
        {
            if (row + wordToFind.Length > matrix.Count)
            {
                return false;
            }

            for (int i = 0; i < wordToFind.Length; i++)
            {
                if (char.ToUpper(matrix[row + i][col].Name) != wordToFind[i])
                {
                    return false;
                }
            }

            return true;
        }

        private void SelectWordHorizontal(string wordToFind, List<List<LetterDto>> matrix, int row, int col)
        {
            for (int i = 0; i < wordToFind.Length; i++)
            {
                matrix[row][col + i].Selected = true;
            }
        }

        private void SelectWordVertical(string wordToFind, List<List<LetterDto>> matrix, int row, int col)
        {
            for (int i = 0; i < wordToFind.Length; i++)
            {
                matrix[row + i][col].Selected = true;
            }
        }
    }
}
