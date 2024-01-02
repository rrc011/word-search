using Finder.Models;
using System.Text.Json;

namespace Finder.Repository.Tests
{
    public class WordFinderTests
    {
        private WordFinder wordFinder;
        private AlphabetSoupResultDto soup;

        [SetUp]
        public void Setup()
        {
            string contentJson = File.ReadAllText("../../../data.json");
            soup = JsonSerializer.Deserialize<AlphabetSoupResultDto>(contentJson);
            wordFinder = new WordFinder(soup);
        }

        [Test]
        public void GenerateMatrix_ReturnsValidMatrix()
        {
            // Arrange
            int matrixLength = 10;

            // Act
            var result = wordFinder.GenerateMatrix(matrixLength);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Matrix);
            Assert.IsNotNull(result.Words);
            Assert.AreEqual(matrixLength, result.Matrix.Count);
            Assert.AreEqual(matrixLength, result.Matrix[0].Count); // Assuming it's a square matrix
            Assert.AreEqual(matrixLength, result.Words.Count);
        }

        [Test]
        public void FindAndMatch_ReturnsFalse()
        {
            // Arrange
            var word = new List<LetterDto>
            {
                new LetterDto { RowIndex = 5, ColIndex = 0, Name = 'W' },
                new LetterDto { RowIndex = 6, ColIndex = 0, Name = 'A' },
                new LetterDto { RowIndex = 7, ColIndex = 0, Name = 'N' },
                new LetterDto { RowIndex = 8, ColIndex = 0, Name = 'T' },
            };

            // Act
            wordFinder.GenerateMatrix(15);
            var result = wordFinder.FindAndMatch(word);

            // Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void FindAndMatch_ReturnsTrue()
        {
            // Arrange
            var word = new List<LetterDto>
            {
                new LetterDto { RowIndex = 5, ColIndex = 0, Name = 'W' },
                new LetterDto { RowIndex = 6, ColIndex = 0, Name = 'A' },
                new LetterDto { RowIndex = 7, ColIndex = 0, Name = 'T' },
                new LetterDto { RowIndex = 8, ColIndex = 0, Name = 'E' },
                new LetterDto { RowIndex = 9, ColIndex = 0, Name = 'R' },
                new LetterDto { RowIndex = 10, ColIndex = 0, Name = 'M' },
                new LetterDto { RowIndex = 11, ColIndex = 0, Name = 'E' },
                new LetterDto { RowIndex = 12, ColIndex = 0, Name = 'L' },
                new LetterDto { RowIndex = 13, ColIndex = 0, Name = 'O' },
                new LetterDto { RowIndex = 14, ColIndex = 0, Name = 'N' },
            };

            // Act
            wordFinder.GenerateMatrix(15);
            var result = wordFinder.FindAndMatch(word);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void FindAndSelectWord()
        {
            // Arrange
            string wordToFind = "WATERMELON";

            // Act
            wordFinder.GenerateMatrix(15);
            var result = wordFinder.FindAndSelectWord(wordToFind);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
