using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace WebApiPersonas
{
    public class PersonaCrud
    {
        public static string cadena = "Server=CARAPRCTIFSD152\\SQLEXPRESS;Database=BD_API;User Id=ADSI2278769;Password=12345;";
        public static SqlConnection cnn = new SqlConnection(cadena);

        //funcion insertar
        //retorno un json
        public string func_insertarpersona(string name, long cel)
        {
            SqlDataAdapter adap = new SqlDataAdapter("PA_INSERTAPERSONA", cnn);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@nom", name);
            adap.SelectCommand.Parameters.AddWithValue("@cel", cel);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            return "Agregado";
            //Para utilizarlo poner en el url "/insertarPersona?name=juan&cel=6565"
        }

        //funcion actualizar persona
        //retorno un json
        public string func_actualizarpersona(long id, string name, long cel)
        {
            SqlDataAdapter adap = new SqlDataAdapter("PA_EDITARPERSONA", cnn);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@id", id);
            adap.SelectCommand.Parameters.AddWithValue("@nom",name);
            adap.SelectCommand.Parameters.AddWithValue("@cel", cel);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            return "Editado";
            //Para utilizarlo poner en el url "/actualizarpersona?id=1&name=juan&cel=6565"
        }

        //funcion seleccionar persona
        //retorno un json
        public string func_seleccionarpersona(long id)
        {
            SqlDataAdapter adap = new SqlDataAdapter("PA_SELECTPERSONA", cnn);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            return DataTableToJSONWithStringBuilder(dt);
            //Para utilizarlo poner en el url "/seleccionarpersona?id=1"
        }

        //funcion eliminar persona
        //retorno un json
        public string func_eliminarpersona(long id)
        {
            SqlDataAdapter adap = new SqlDataAdapter("PA_ELIMINARPERSONA", cnn);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            return "Eliminado";
            //Para utilizarlo poner en el url "/actualizarpersona?id=1"
        }

        //funcion traer tablas
        //retorno un json
        public string func_traertodo()
        {
            SqlDataAdapter adap = new SqlDataAdapter("PA_SELECCIONARTODOS",cnn);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adap.Fill(dt);
            return DataTableToJSONWithStringBuilder(dt);
        }

        //Codigo para convertir DataTable a JSON
        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }
    }
}
