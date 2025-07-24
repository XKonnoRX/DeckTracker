using AForge.Math.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckTracker
{
    internal class UserOutput:Panel
    {
        public TextBox Number { get; set; }
        public TextBox Name { get; set; }
        public TextBox Hand { get; set; }
        public TextBox Chance { get; set; }
        public TextBox Action { get; set; }
        public TextBox Result { get; set; }
        private MainForm Form1 { get; set; }
        public CardsBox cardsBox;
        public UserOutput(int number, bool mine, MainForm parent) //строка вывода параметров пользователя
        {
            Form1 = parent;
            
            Size = new Size(430, 15);


            //Number
            Number = new TextBox();
            Number.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(57)))));
            Number.BorderStyle = BorderStyle.None;
            Number.Font = new Font("MS Reference Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Number.ReadOnly = true;
            if(mine)
                Number.ForeColor = Color.Green;
            else
            Number.ForeColor = SystemColors.Menu;
            Number.Location = new Point(8, 0);
            Number.Size = new Size(26, 15);
            Number.TabIndex = 54;
            Number.Text = "#"+number;
            Number.TextAlign = HorizontalAlignment.Center;
            // Name
            Name = new TextBox();
            Name.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(57)))));
            Name.BorderStyle = BorderStyle.None;
            Name.Font = new Font("MS Reference Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Name.ReadOnly = !mine;
            if (mine)
                Name.ForeColor = Color.Green;
            else
                Name.ForeColor = SystemColors.Menu;
            Name.Location = new Point(33, 0);
            Name.Size = new Size(76, 15);
            Name.TabIndex = 4;
            Name.Text = "NoName";
            Name.TextAlign = HorizontalAlignment.Center;
            // 
            // Hand
            // 
            Hand = new TextBox();
            Hand.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(57)))));
            Hand.BorderStyle = BorderStyle.None;
            Hand.Font = new Font("MS Reference Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Hand.ReadOnly = true;
            Hand.ForeColor = SystemColors.Menu;
            Hand.Location = new Point(114, 0);
            Hand.Size = new Size(101, 15);
            Hand.TabIndex = 5;
            Hand.Text = "----------";
            Hand.TextAlign = HorizontalAlignment.Center;
            Hand.DoubleClick += new System.EventHandler(Hand_DoubleClick);
            // 
            // Chance
            // 
            Chance = new TextBox();
            Chance.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(57)))));
            Chance.BorderStyle = BorderStyle.None;
            Chance.Font = new Font("MS Reference Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Chance.ReadOnly = true;
            Chance.ForeColor = SystemColors.Menu;
            Chance.Location = new Point(221, 0);
            Chance.Size = new Size(62, 15);
            Chance.TabIndex = 6;
            Chance.Text = "0.0%";
            Chance.TextAlign = HorizontalAlignment.Center;
            // 
            // Action
            // 
            Action = new TextBox();
            Action.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(57)))));
            Action.BorderStyle = BorderStyle.None;
            Action.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Action.ReadOnly = true;
            Action.ForeColor = SystemColors.Menu;
            Action.Location = new Point(289, 0);
            Action.Size = new Size(59, 15);
            Action.TabIndex = 7;
            Action.Text = "NOTHING";
            Action.TextAlign = HorizontalAlignment.Center;
            // 
            // Result
            // 
            Result = new TextBox();
            Result.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(57)))));
            Result.BorderStyle = BorderStyle.None;
            Result.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Result.ReadOnly = true;
            Result.ForeColor = SystemColors.Menu;
            Result.Location = new Point(354, 0);
            Result.Size = new Size(59, 15);
            Result.TabIndex = 8;
            Result.Text = "NOTHING";
            Result.TextAlign = HorizontalAlignment.Center;

            Controls.Add(Number);
            Controls.Add(Name);
            Controls.Add(Hand);
            Controls.Add(Chance);
            Controls.Add(Action);
            Controls.Add(Result);

        }

        private void Hand_DoubleClick(object sender, EventArgs e) //визуальное отображение карт
        {
            if (Hand.Text != "")
            {
                if (cardsBox != null)
                    cardsBox.Close();
                cardsBox = new CardsBox(new Point(Form1.Location.X + Hand.Location.X, Form1.Location.Y + Location.Y - 80));
                cardsBox.SetCards(Hand.Text);
                cardsBox.Show();
            }
            
        }
    }
}
