using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
//for connection string
using System.Configuration;
//for read MyfirstMVC_Ado Model Class
using MyFirstMVC_ADO.Models;
using System.Data;


namespace MyFirstMVC_ADO.DAL
{
    public class ProductDAL
    {
        //Connect with database with the help of connectionstring
        string constring = ConfigurationManager.ConnectionStrings["MvcAdoConnection"].ToString();

        //Create function to get All Product
        public List<Product> GetAllProducts()
        {
            //create a object To hold the Product List
            List<Product> ProductsList = new List<Product>();

            //Call the storedProcedure to read the data from databse

            using(SqlConnection conn = new SqlConnection(constring))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.CommandText = "GetAllProduct";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();

                conn.Open();
                adapter.Fill(dataTable);
                conn.Close();
                //For read the data from datatable rows
                foreach(DataRow dr in dataTable.Rows)
                {
                    ProductsList.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        QTY = Convert.ToInt32(dr["QTY"]),
                        Remarks = dr["Remarks"].ToString()

                    });

                }


            }




                return ProductsList;


        }


        public bool SaveProduct(Product product)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(constring))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SaveProduct";
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@QTY", product.QTY);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);


                conn.Open();
                id = command.ExecuteNonQuery();
                conn.Close();

            }
            if (id > 0) { return true; }
            else {
                return false;
        }
        }



        public List<Product> GetProductListById( int ProductId)
        {
            List<Product> ProductList = new List<Product>();

            using(SqlConnection conn=new SqlConnection(constring))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetProductById";
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();

                conn.Open();
                adapter.Fill(dataTable);
                conn.Close();


                foreach(DataRow dr in dataTable.Rows)
                {

                    ProductList.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        QTY = Convert.ToInt32(dr["QTY"]),
                        Remarks = dr["Remarks"].ToString(),
                    });
                }

            }

            return ProductList;
        }

        public bool UpdateProduct(Product product)

        {
            int IsUpdated = 0;
            using(SqlConnection conn=new SqlConnection(constring))
            {



                SqlCommand cmd = new SqlCommand("UpdateProduct", conn);

                //SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "UpdateProduct";
                cmd.Parameters.AddWithValue("ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@QTY", product.QTY);
                cmd.Parameters.AddWithValue("@Remarks", product.Remarks);


                conn.Open();
                IsUpdated = cmd.ExecuteNonQuery();
                conn.Close();

            }
            if(IsUpdated > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteProduct( int ProductId)

        {
            int IsDeleted = 0;
            using (SqlConnection conn = new SqlConnection(constring))
            {



                SqlCommand cmd = new SqlCommand("DeleteProduct", conn);

                //SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "UpdateProduct";
                cmd.Parameters.AddWithValue("ProductId", ProductId);
              

                conn.Open();
                IsDeleted = cmd.ExecuteNonQuery();
                conn.Close();

            }
            if (IsDeleted > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}