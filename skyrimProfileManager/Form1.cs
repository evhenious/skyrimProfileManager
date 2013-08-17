using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace skyrimProfileManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        profiler oProfiler = new profiler();

        private void Form1_Load(object sender, EventArgs e)
        {
            //проверяем наличие профилей в папке
            foreach (DirectoryInfo dir in oProfiler.profiles)
            {
                comboBox1.Items.Add(dir);
            }
            if (comboBox1.Items.Count < 1)
            {
               // comboBox1.Enabled = false;
                lInfo.Text = "Готовых профилей не обнаружено...";
            }
            //проверяем, есть ли активный профиль
            FileInfo[] file = oProfiler.savesDir.GetFiles("_*");
            if (file.Length != 0)
            {
                lInfo.Text = "Активный профиль: " + file.GetValue(0);
                oProfiler.activeProfile = file.GetValue(0).ToString();
            }
            else
                lInfo.Text = "Нет активного профиля";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button1.Visible = true;
            button3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //делаем backup активного ранее профиля
            if (oProfiler.activeProfile != "")
            {
                foreach (FileInfo file in oProfiler.savesDir.GetFiles())
                {
                    file.CopyTo(oProfiler.currentDir.ToString() + "\\" + oProfiler.activeProfile + "\\" + file.Name, true);
                    file.Delete();
                }
            }

            //копируем файлы выбранного профиля в 'saves'
            DirectoryInfo selectedProfile = new DirectoryInfo(comboBox1.SelectedItem.ToString());
            foreach (FileInfo file in selectedProfile.GetFiles())
            {
                file.CopyTo(oProfiler.currentDir.ToString()+".\\saves\\"+file.Name, true);
            }
            lInfo.Text = "Профиль " + selectedProfile.ToString() + " активирован";
            oProfiler.activeProfile = selectedProfile.ToString();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.FlatAppearance.BorderColor = Color.DarkBlue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.FlatAppearance.BorderColor = Color.FromArgb(224,224,224);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderColor = Color.DarkCyan;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (oProfiler.createProfile(textBox1.Text) == 1)
                {
                    if (oProfiler.profiles.Length != 0)
                    {
                        comboBox1.Items.Add(oProfiler.profiles.GetValue(oProfiler.profiles.Length - 1));
                    }
                    else
                    {
                        comboBox1.Items.Add(oProfiler.profiles.GetValue(0));
                        comboBox1.Refresh();
                    }
                    label2.Text = "Профиль создан";
                }
                else
                {
                    label2.Text = "Профиль уже существует";
                }
            }
            else
            {
                label2.Text = "Введите имя профиля";
            }
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderColor = Color.Red;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string nameToDel = comboBox1.SelectedItem.ToString();
            if (oProfiler.activeProfile != nameToDel)
            {
                oProfiler.deleteProfile(nameToDel);
                comboBox1.Items.Remove(comboBox1.SelectedItem);
                lInfo.Text = "Профиль удален";
            }
            else
            {
                lInfo.Text = "Нельзя удалить активный профиль";
            }
            
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.FlatAppearance.BorderColor = Color.Green;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Visible == true)
            {
                textBox2.Visible = false;
            }
            else
                textBox2.Visible = true;
        }
    }
}
