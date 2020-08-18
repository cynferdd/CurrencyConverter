
namespace Logger.Abstraction
{
    public interface ILogger
    {
        void FilePathNeeded();

        void MinimumAmountOfLinesNotReached();

        void FirstLineWrongAmountOfFields();

        void FirstLineWrongFieldsFormat();

        void SecondLineWrongNotPositiveInt();

        void WrongAmountOfLines();

        void WrongChangeDataFormat();

        void WrongDataLineFormat(string line);

        void FileNotFound();

        void NoConversionPathFound();

        void NoDataRetrievedFromFile();

        void Write(string message);
    }
}
