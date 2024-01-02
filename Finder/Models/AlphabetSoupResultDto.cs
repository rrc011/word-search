namespace Finder.Models
{
    public class AlphabetSoupResultDto
    {
        public List<WordsDto> Words { get; set; }
        public List<List<LetterDto>> Matrix { get; set; }
    }

    public partial class LetterDto
    {
        public char Name { get; set; }
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
        public bool Selected { get; set; }
    }
    
    public partial class WordsDto
    {
        public string Name { get; set; }
        public bool Selected { get; set; }
        public bool Found { get; set; }
    }
}
