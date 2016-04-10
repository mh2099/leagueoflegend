namespace lolLib.EF
{
    using System;

    public static class DBConnection
    {
        private static String _host;
        private static Int32 _port;
        private static String _username;
        private static String _password;
        private static String _table;

        public static void SetConnection(String Host, Int32 Port, String Username, String Password, String Table)
        {
            _host = Host;
            _port = Port;
            _username = Username;
            _password = Password;
            _table = Table;
        }

        public static String ShowConnectionString()
        {
            return $"metadata=res://*/EF.lolModel.csdl|res://*/EF.lolModel.ssdl|res://*/EF.lolModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"server={_host};port={_port};user id={_username};password={_password};persist security info=True;database={_table};allow zero datetime=True;convert zero datetime=True\"";
        }
    }
}