using Finder.Models;

namespace Finder.Repository
{
    public interface IWordFinder
    {
        AlphabetSoupResultDto GenerateMatrix(int matrixLength);
        AlphabetSoupResultDto GetMatrix();
        bool FindAndMatch(List<LetterDto> word);
        bool FindAndSelectWord(string wordToFind);
    }
}
