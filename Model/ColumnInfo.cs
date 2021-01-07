using System.ComponentModel;

namespace daterApp
{
    class ColumnInfo : INotifyPropertyChanged
    {
        string columnTitle;
        string dataType;
        string parameters;

        int missingValues;

        public string ColumnTitle
        {
            get { return columnTitle; }
            set { columnTitle = value; }
        }

        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        public int MissingValues
        {
            get { return missingValues; }
            set { missingValues = value; }
        }

        public string Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public ColumnInfo()
        {
        }

        public ColumnInfo(string columnTitle, string dataType, int missingValues, string parameters)
        {
            ColumnTitle = columnTitle;
            DataType = dataType;
            MissingValues = missingValues;
            Parameters = parameters;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
