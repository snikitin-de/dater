namespace dater
{
    class DatasetMetaInfo
    {
        int rowCount;
        string outputFile;
        string separator;


        public int RowCount
        {
            get { return rowCount; }
            set { rowCount = value; }
        }

        public string Separator
        {
            get { return separator; }
            set { separator = value; }
        }

        public string OutputFile
        {
            get { return outputFile; }
            set { outputFile = value; }
        }

        public DatasetMetaInfo(int rowCount, string separator, string outputFile)
        {
            RowCount = rowCount;
            Separator = separator;
            OutputFile = outputFile;
        }
    }
}
