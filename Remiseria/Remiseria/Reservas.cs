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
    public partial class Reservas : Form
    {
        public Reservas()
        {
            InitializeComponent();
        }

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        private void Reservas_Load(object sender, EventArgs e)
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
                string CommandText = "select CODIGO, FECHA, CLIENTE, HORA_RESERVA, DIRECCION, DESTINO, OBSERVACIONES from reservas where FECHA like ('" + txtBusqueda.Text + "%')";
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
                if (MessageBox.Show("Seguro desea agregar reserva ?", "Agregar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string txtQuery = "insert into reservas (FECHA, CLIENTE, HORA_RESERVA, DIRECCION, DESTINO, OBSERVACIONES) values ('" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "' )";
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
            textBox7.Text = "";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Seguro desea modificar reserva ?", "Modificar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string txtQuery = "update reservas Set FECHA = '" + textBox2.Text + "', CLIENTE = '" + textBox3.Text + "', HORA_RESERVA ='" + textBox4.Text + "', DIRECCION = '" + textBox5.Text + "', DESTINO = '" + textBox6.Text + "', OBSERVACIONES = '" + textBox7.Text + "' where CODIGO = '" + textBox1.Text + "' ";
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
            textBox7.Text = "";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Seguro desea eliminar reserva ?", "Eliminar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string txtQuery = "delete from reservas where CODIGO = '" + textBox1.Text + "'";
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
            textBox7.Text = "";
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
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

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
    }
}

    

    
    



