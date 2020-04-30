﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Remiseria
{
    public partial class Choferes : Form
    {
        public Choferes()
        {
            InitializeComponent();
        }

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        private void Choferes_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        //conexion a la bd
        private void SetConnection()
        {
            try
            {
                sql_con = new SQLiteConnection
                    ("Data Source = database.db; version = 3; New = False; Compress = True;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //ejecutar consulta
        private void ExecuteQuery(string txtQuery)
        {
            try
            {
                SetConnection();
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = txtQuery;
                sql_cmd.ExecuteNonQuery();
                sql_con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        //cargar bd
        private void LoadData()
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetConnection();
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                string CommandText = "select CODIGO, APELLIDO, DIRECCION, TELEFONO, AUTO, PATENTE from choferes where APELLIDO like ('" + txtBusqueda.Text + "%')";
                DB = new SQLiteDataAdapter(CommandText, sql_con);
                DS.Reset();
                DB.Fill(DS);
                DT = DS.Tables[0];
                dataGridView1.DataSource = DT;
                sql_con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Seguro desea agregar chofer ?", "Agregar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string txtQuery = "insert into choferes (APELLIDO, DIRECCION, TELEFONO, AUTO, PATENTE) values ('" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "' )";
                    ExecuteQuery(txtQuery);
                    LoadData();
                    MessageBox.Show("Registro agregado correctamente !!");
                }
                else { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Seguro desea modificar chofer ?", "Modificar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string txtQuery = "update choferes Set APELLIDO = '" + textBox2.Text + "', DIRECCION = '" + textBox3.Text + "', TELEFONO = '" + textBox4.Text + "', AUTO = '" + textBox5.Text + "', PATENTE = '" + textBox6.Text + "' where CODIGO = '" + textBox1.Text + "' ";
                    ExecuteQuery(txtQuery);
                    LoadData();
                    MessageBox.Show("Registro modificado correctamente !!");
                }
                else { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Seguro desea eliminar chofer ?", "Eliminar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string txtQuery = "delete from choferes where CODIGO = '" + textBox1.Text + "'";
                    ExecuteQuery(txtQuery);
                    LoadData();
                    MessageBox.Show("Registro eliminado correctamente !!");
                }
                else { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            
            }

            catch (Exception)
            {
                MessageBox.Show("Elija fila correcta");
            }



        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Bitmap objBmp = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
                dataGridView1.DrawToBitmap(objBmp, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
                e.Graphics.DrawImage(objBmp, 10, 100);
                e.Graphics.DrawString(label1.Text, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Black, new Point(300, 30));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnViajes_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro desea ver viajes ?", "Ver viajes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Chofer chofer = new Chofer();
                chofer.Show();
            }
            else { }
        }

    }

}

   

    

    