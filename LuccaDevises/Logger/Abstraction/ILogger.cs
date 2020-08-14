
namespace Logger.Abstraction
{
    public interface ILogger
    {
        void MinimumAmountOfLinesNotReached();
        void FirstLineWrongAmountOfFields();
        void FirstLineWrongFieldsFormat();
        void SecondLineWrongNotPositiveInt();
        void WrongAmountOfLines();
        void WrongChangeDataFormat();
        void WrongDataLineFormat(string line);

        void FileNotFound();

        void NoChangesPathFound();

        void NoDataRetrievedFromFile();
    }
}
