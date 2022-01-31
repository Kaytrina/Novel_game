using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;


namespace Индивидуальное_задание
{

    public partial class Form1 : Form
    {

        public int stage = 2; //№ строки с которой начинается текст
        public string save;
        public string[] content = new string[98]; //сценарий(сюжет)
        SoundPlayer player = new SoundPlayer(); //переменная плеера музыки       

        public Form1()
        {
            InitializeComponent();
            /*ПОДКЛЮЧЕНИЕ ЗВУКА*/         
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\сюжет.wav";
            player.PlayLooping();
            player.Play();                     

            /*ПОДКЛЮЧЕНИЕ текстового файла*/
            StreamReader sr = new StreamReader(@"сюжет.txt");  //подключение файла с сюжетом
            for (int i = 0; i < 98; i++)
            {
                content[i] = sr.ReadLine();
            }
            sr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = new Button(); //создание кнопки для чтения истории
            pictureBox1.Dispose(); //скрыть название           
            button1.Dispose(); //скрыть кнопку начала игры
            trackBar1.Dispose();  //скрыть регулировку музыки
            this.BackgroundImage = Image.FromFile(@"повествование.png");

            /*параметры новой кнопки*/
            b.BackColor = System.Drawing.Color.Transparent;
            b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            b.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            b.ForeColor = System.Drawing.Color.Crimson;
            b.Location = new System.Drawing.Point(980, 600);
            b.Name = "btn";
            b.Size = new System.Drawing.Size(55, 35);
            b.TabIndex = 2;
            b.Text = ">>";
            b.UseVisualStyleBackColor = false;
            b.Click += new System.EventHandler(this.btn_Click);
            this.Controls.Add(b);

            label_d(content[1], 20, 580, "str");  //вывод текста

            PictureBox pb2 = new PictureBox();
            this.Controls.Add(pb2);
            pb2.Image = Image.FromFile(@"подложка.jpg");
            pb2.Size = new System.Drawing.Size(1063, 110);
            pb2.Location = new System.Drawing.Point(5, 560);           
        }

        public void draw(string path, int x, int y, string name)
        {
            PictureBox pb = new PictureBox();
            pb.Image = Image.FromFile(path);
            pb.Location = new Point(x, y); 
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            pb.BackColor = Color.Transparent;
            pb.Name = name;
            this.Controls.Add(pb);
        }

        public void label_d(string text, int x, int y, string name)
        {
            Label l = new Label(); //переменная текста
            l.Text = text;            
            l.Location = new Point(x, y);
            l.ForeColor = Color.Crimson;
            l.BackColor = Color.MistyRose;
            l.Font = new System.Drawing.Font("Segoe Script", 13f, FontStyle.Bold);
            l.AutoSize = true;
            l.MaximumSize = new Size(950, 0);
            l.Name = name;
            this.Controls.Add(l);
        }
       
       /* public void draw2_Paint(object sender, EventArgs e)
        {
            PictureBox pb2 = new PictureBox();
            

        }*/

        private void btn_Click(object sender, EventArgs e)
        {           
            this.Controls["str"].Text = content[stage++]; //вывод текста по строчно  

            if (content[stage] == "...отлично")
            {
                this.BackgroundImage = Image.FromFile(@"стол.png");                
            }

            if (content[stage] == "Ух...")
            {
                this.BackgroundImage = Image.FromFile(@"комната.jpg");
                draw(@"гг_шок.png", 90, 30, "GGh");                
            }

            if (content[stage] == "Феликс!!! Ты снова подменил мои колбы?! Это уже не смешно!")
            {
                this.Controls["GGh"].Dispose();
                draw(@"гг_злость.png", 90, 30, "GGz");
            }

            if (content[stage] == "ФЕЛИКС: Всего-то наклейки спутал")
            {
                draw(@"феликс_ехидный.png", 600, 260, "Felix_e");
            }

            if (content[stage] == "Хааа... ну, возможно")
            {
                this.Controls["GGz"].Dispose();
                draw(@"гг_улыбка.png", 90, 30, "GGs");
            }

            if (content[stage] == "О, уже 12! Нам пора прогуляться! Пошли Феликс!")
            {
                this.Controls["GGs"].Dispose();
                draw(@"гг_слова.png", 90, 30, "GG");
                this.Controls["Felix_e"].Dispose();
                draw(@"феликс.png", 600, 260, "Felix");
            }

            if (content[stage] == "~Спустя некоторое время сборов и выслушивания причитаний говорящего кота, Пьера вышла из лавки со своим спутником.~")
            {
                this.BackgroundImage = Image.FromFile(@"дом.jpg");
            }


            

            //ВЫБОР
            if (content[stage] == "Куда бы направиться?")
            {
                panel1.Show();
                this.Controls["btn"].Hide();
                this.Controls["GG"].Dispose();
                this.Controls["Felix"].Dispose();
            }

            if (content[stage] == "Конец")
            {              
                this.Controls["btn"].Hide();
                try
                {
                    this.Controls["str"].Dispose();
                    /*this.Controls["GG"].Dispose();
                    this.Controls["Felix"].Dispose();*/
                } catch
                { }
            }

            //соответствие строки и сцены           
            try
            {
                this.Controls["str"].Text = content[stage];
            }
            catch
            {
                MessageBox.Show("Спасибо, что прочли мою новеллу! До новых встреч 	ʕ ᵔᴥᵔ ʔ❤");
                Application.Exit();
            }


            /*КОНЦОВКА 1*/
            if (content[stage] == "Отправимся в пекарню! Мистер Акменос уже должен был испечь дынных булочек!")
            {
                draw(@"гг_слова.png", 90, 30, "GG");
                draw(@"феликс.png", 600, 260, "Felix");
            }

            if (content[stage] == "Доброе утро, мистер Акменос!")
            {
                this.BackgroundImage = Image.FromFile(@"пекарня.jpg");
            }

            if (content[stage] == "М-Р АКМЕНОС: О, Пьера, ты как раз вовремя! Винни куда-то убежала, и мне необходимо её найти!")
            {
                this.Controls["Felix"].Dispose();
                draw(@"м-р Акменос.png", 600, 1, "Akmen");
            }

            if (content[stage] == "Тогда мы пошли.")
            {
                this.Controls["GG"].Dispose();
                draw(@"гг_улыбка.png", 90, 30, "GGs");
            }

            if (content[stage] == "~И с этими словами юная ведьма вышла из магазина.~")
            {
                this.BackgroundImage = Image.FromFile(@"город.jpg");
                this.Controls["Akmen"].Dispose();
                draw(@"феликс.png", 600, 260, "Felix");
            }

            if (content[stage] == "~Спустя некоторое время наша героиня долетела до маяка, и шум накатывающих волн моря ласкал её уши.~")
            {
                this.Controls["GGs"].Dispose();
                this.Controls["Felix"].Dispose();
                this.BackgroundImage = Image.FromFile(@"маяк.png");
            }

            if (content[stage] == "О, я её вижу! Вон там наверху!")
            {
                draw(@"гг_слова.png", 90, 30, "GG");
            }

            if (content[stage] == "ВИННИ: О Пьера, Феликс, что вы тут делаете?")
            {
                draw(@"Винни.png", 600, 175, "Vinny");
            }

            if (content[stage] == "ФЕЛИКС: Полетели БЫСТРЕЕ!")
            {
                this.Controls["Vinny"].Dispose();
                draw(@"феликс.png", 600, 260, "Felix");
            }

            if (content[stage] == "--КОНЦОВКА 1: Любители булочек--")
            {
                this.Controls["GG"].Dispose();
                this.Controls["Felix"].Dispose();
                this.BackgroundImage = Image.FromFile(@"кц1.png");
            }


            /*КОНЦОВКА 2*/
            if (content[stage] == "Полетели в цветочный, там должно быть много новых семян привезли.")
            {
                draw(@"гг_слова.png", 90, 30, "GG");
                draw(@"феликс.png", 600, 260, "Felix");
            }

            if (content[stage] == "Мари, доброго утречка!")
            {
                this.BackgroundImage = Image.FromFile(@"цветы.jpg");
            }

            if (content[stage] == "МАРИ: Пьера, утро доброе. Ты за новыми семенами?")
            {
                this.Controls["Felix"].Dispose();
                draw(@"Мари.png", 600, 160, "Mari");
            }

            if (content[stage] == "ФЕЛИКС: Не было такого!")
            {
                this.Controls["Mari"].Dispose();
                draw(@"феликс.png", 600, 260, "Felix");
            }

            if (content[stage] == "МАРИ: Ахпахп, сейчас-сейчас, их нужно только разобрать. Подсобишь?")
            {
                this.Controls["Felix"].Dispose();
                draw(@"Мари.png", 600, 160, "Mari");
                this.Controls["GG"].Dispose();
                draw(@"гг_улыбка.png", 90, 30, "GGs");
            }

            if (content[stage] == "~Спустя 35 разобранных коробок с разным ассортиментом, он наконец был разобран. На радостях от помощи подруге Пьера и забыла зачем пришла.")
            {
                this.Controls["GGs"].Dispose();
                this.Controls["Mari"].Dispose();
                this.BackgroundImage = Image.FromFile(@"город.jpg");
            }

            if (content[stage] == "МАРИ: Звёздочка, погоди! Это тебе, возьми!")
            {               
                draw(@"Мари.png", 600, 160, "Mari");                
                draw(@"гг_улыбка.png", 90, 30, "GGs");
            }

            if (content[stage] == "--КОНЦОВКА 2: Кошачье счастье--")
            {
                this.Controls["GGs"].Dispose();
                this.Controls["Mari"].Dispose();
                this.BackgroundImage = Image.FromFile(@"кц2.png");
            }


            /*КОНЦОВКА 3*/
            if (content[stage] == "Заглянем-ка к Сержу в таверну! Может там чего интересного происходит.")
            {
                draw(@"гг_слова.png", 90, 30, "GG");
                draw(@"феликс_ехидный.png", 600, 260, "Felix_e");
            }

            if (content[stage] == "Ого, ну и заварушка тут")
            {
                this.BackgroundImage = Image.FromFile(@"таверна.jpg");
            }

            if (content[stage] == "СЕРЖ: Пьера! Лучше времени для твоего прихода и не найдёшь! Проходи!")
            {
                this.Controls["Felix_e"].Dispose();
                draw(@"Серж.png", 600, 150, "Serg");
            }          

            if (content[stage] == "Эй, твоих рук дело?")
            {
                this.Controls["Serg"].Dispose();              
                draw(@"чел.png", 600, 70, "chel");
            }

            if (content[stage] == "А то, что посетители вам тут не мишени! Рассчитывать удар нужно!")
            {
                this.Controls["GG"].Dispose();
                draw(@"гг_злость.png", 90, 30, "GGz");
            }

            if (content[stage] == "ФЕЛИКС: Зря ты это парень начал...")
            {
                this.Controls["chel"].Dispose();
                draw(@"феликс.png", 600, 260, "Felix"); 
            }

            if (content[stage] == "АВАНТЮРИСТ: Аахаха, а то что? Что она мне сделает?")
            {
                this.Controls["Felix"].Dispose();
                draw(@"чел.png", 600, 70, "chel");
            }

            if (content[stage] == "~Но стоило только парню посмотрел на ведьму смех начал стихать. Пьера уже парила над кругом с пиктограммой и шептала заклинание.")
            {
                this.Controls["chel"].Dispose();
                this.Controls["GGz"].Dispose();
            }

            if (content[stage] == "ФЕЛИКС: Да, не позавидуешь парню, он теперь дня 3 в отключке пролежит.")
            {
                draw(@"гг_улыбка.png", 90, 30, "GGs");
                draw(@"феликс.png", 600, 260, "Felix");
            }

            if (content[stage] == "--КОНЦОВКА 3: Лучше ведьмочку не зли!--")
            {
                this.Controls["GGs"].Dispose();
                this.Controls["Felix"].Dispose();
                this.BackgroundImage = Image.FromFile(@"кц3.png");
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 100;
            uint volume = 30;           
            trackBar1.Value = (int)(volume & 0xFFFF);
            //SetVolume();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stage = 25;
            panel1.Dispose();
            this.Controls["btn"].Show();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stage = 51;
            panel1.Dispose();
            this.Controls["btn"].Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            stage = 66;
            panel1.Dispose();
            this.Controls["btn"].Show();
        }
    }
}
