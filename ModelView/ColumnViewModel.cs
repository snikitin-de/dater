using System.Collections.ObjectModel;

namespace daterApp
{
    class ColumnViewModel
    {
        private ObservableCollection<ColumnInfo> _columns;

        public ObservableCollection<ColumnInfo> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public ColumnViewModel()
        {
            _columns = new ObservableCollection<ColumnInfo>();
            GenerateColumns();
        }

        private void GenerateColumns()
        {
            _columns.Add(new ColumnInfo("col1", "String", 0, null));
            _columns.Add(new ColumnInfo("col2", "Integer", 0, null));
            _columns.Add(new ColumnInfo("col3", "Float", 0, null));
            _columns.Add(new ColumnInfo("col4", "Datetime", 0, null));
            _columns.Add(new ColumnInfo("col5", "Boolean", 0, null));
            _columns.Add(new ColumnInfo("col6", "Variant", 0, null));
            _columns.Add(new ColumnInfo("col7", "GUID", 0, null));
        }
    }
}
