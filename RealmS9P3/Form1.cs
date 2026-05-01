namespace RealmS9P3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            _previousAction = string.Empty;
            InitializeComponent();
            InitControls();
        }

        private int _horseheadpointer { get; set; }
        private string _previousAction{ get; set; }

        #region Enums
        private enum Direction
        {
            Front = 0,
            Left = 1,
            Back = 2,
            Right = 3
        }

        private enum Ringsection
        {
            NoRing = 0,
            Inner = 1,
            Outer = 2
        }
        #endregion

        #region private Methods
        private void InitControls()
        {
            InitComboBoxes();
            InitPointer();
            InitLabels();
            InitButtonPanel();
        }

        private void InitComboBoxes()
        {
            foreach (var cb in this.Controls.OfType<ComboBox>())
            {
                cb.DataSource = new List<string>() { "0", "1" };
                cb.SelectedIndex = -1;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void InitPointer()
        {
            _horseheadpointer = 0;
            _previousAction = string.Empty;
        }

        private void InitLabels()
        {
            foreach (var lb in this.Controls.OfType<Label>())
            {
                lb.Text = string.Empty;
            }
        }

        private void InitButtonPanel()
        {
            foreach (var gpb in this.Controls.OfType<GroupBox>())
            {
                foreach (var btn in gpb.Controls.OfType<Button>())
                {
                    btn.Text = string.Empty;
                    btn.BackColor = Color.AliceBlue;
                }
            }
        }

        private void ClearComboBoxes()
        {
            foreach (var cb in this.Controls.OfType<ComboBox>())
            {
                cb.SelectedIndex = -1;
            }
        }

        private string GetActionsforRow(ComboBox box1, ComboBox box2, ComboBox box3, ComboBox box4, int sectionId)
        {
            if (box1.SelectedIndex != -1 && box2.SelectedIndex != -1 && box3.SelectedIndex != -1 && box4.SelectedIndex != -1)
            {
                return CalculateSymbol(Convert.ToString(box4.SelectedValue), Convert.ToString(box3.SelectedValue), Convert.ToString(box2.SelectedValue), Convert.ToString(box1.SelectedValue), sectionId);
            }
            else
            {
                return string.Empty;
            }
        }

        private string CalculateSymbol(string a, string b, string c, string d, int sectionId)
        {
            string combined = a + b + c + d;
            string instuction = string.Empty;
 
            int aoeRelativeDirection = 0;
            int ringsectionvalue = 0;
            bool isCastAoe = false;

            //if code is repeat, take the previousaction value, otherwise save current value to _previousAction for later use
            if (combined == "1111")
            {
                combined = _previousAction;
                instuction = "重复一次";
            }
            else
            {
                _previousAction = combined;
            }

            if (combined == "0001")
            {
                isCastAoe = true;
                aoeRelativeDirection = 0;
            }
            else if (combined == "0010")
            {
                isCastAoe = true;
                ringsectionvalue = 1;
            }
            else if (combined == "0011")
            {
                isCastAoe = true;
                ringsectionvalue = 2;
            }
            else if (combined == "0100")
            {
                _horseheadpointer += 2;
                instuction = "马转身180";
            }
            else if (combined == "0101")
            {
                isCastAoe = true;
                aoeRelativeDirection = 2;
            }
            else if (combined == "1000")
            {
                _horseheadpointer += 1;
                instuction = "马左转身90";
            }
            else if (combined == "1001")
            {
                isCastAoe = true;
                aoeRelativeDirection = 1;
            }
            else if (combined == "1100")
            {
                _horseheadpointer += 3;
                instuction = "马右转身90";
            }
            else if (combined == "1101")
            {
                isCastAoe = true;
                aoeRelativeDirection = 3;
            }
            else if (combined == "1111")
            {
                combined = _previousAction;
                instuction = "重复一次";
            }
            else instuction = string.Empty;


            //calculate absolute direction - where horse is heading
            int absolutedirectionvalue = 0;
            absolutedirectionvalue = (_horseheadpointer + aoeRelativeDirection) % 4;

            Direction direction = (Direction)absolutedirectionvalue;
            Ringsection ringsection = (Ringsection)ringsectionvalue;

            //pass absolute direction to calculate aoe area and safe zones
            if (isCastAoe)
            {
                instuction = CastAoe(direction, ringsection, sectionId);
            }
            return instuction;
        }

        private string CastAoe(Direction Direction, Ringsection ringsection, int sectionId)
        {
            string Instruction = string.Empty;

            if (ringsection == Ringsection.Inner)
            {
                if (sectionId == 1)
                {
                    button6.BackColor = Color.Red;
                    button7.BackColor = Color.Red;
                    button10.BackColor = Color.Red;
                    button11.BackColor = Color.Red;
                }
                else if (sectionId == 2)
                {
                    button25.BackColor = Color.Red;
                    button28.BackColor = Color.Red;
                    button29.BackColor = Color.Red;
                    button30.BackColor = Color.Red;
                }
                else if (sectionId == 3)
                {
                    button41.BackColor = Color.Red;
                    button44.BackColor = Color.Red;
                    button45.BackColor = Color.Red;
                    button46.BackColor = Color.Red;
                }
                Instruction = "外圈安全";
            }
            else if (ringsection == Ringsection.Outer)
            {
                if (sectionId == 1)
                {
                    button1.BackColor = Color.Red;
                    button2.BackColor = Color.Red;
                    button3.BackColor = Color.Red;
                    button4.BackColor = Color.Red;
                    button5.BackColor = Color.Red;
                    button8.BackColor = Color.Red;
                    button9.BackColor = Color.Red;
                    button12.BackColor = Color.Red;
                    button13.BackColor = Color.Red;
                    button14.BackColor = Color.Red;
                    button15.BackColor = Color.Red;
                    button16.BackColor = Color.Red;
                }
                else if (sectionId == 2)
                {
                    button17.BackColor = Color.Red;
                    button18.BackColor = Color.Red;
                    button19.BackColor = Color.Red;
                    button20.BackColor = Color.Red;
                    button21.BackColor = Color.Red;
                    button22.BackColor = Color.Red;
                    button23.BackColor = Color.Red;
                    button24.BackColor = Color.Red;
                    button26.BackColor = Color.Red;
                    button27.BackColor = Color.Red;
                    button31.BackColor = Color.Red;
                    button32.BackColor = Color.Red;
                }
                else if (sectionId == 3)
                {
                    button33.BackColor = Color.Red;
                    button34.BackColor = Color.Red;
                    button35.BackColor = Color.Red;
                    button36.BackColor = Color.Red;
                    button37.BackColor = Color.Red;
                    button38.BackColor = Color.Red;
                    button39.BackColor = Color.Red;
                    button40.BackColor = Color.Red;
                    button42.BackColor = Color.Red;
                    button43.BackColor = Color.Red;
                    button47.BackColor = Color.Red;
                    button48.BackColor = Color.Red;
                }
                Instruction = "内圈安全";
            }
            else if (ringsection == Ringsection.NoRing)
            {
                if (Direction == Direction.Front)
                {
                    if (sectionId == 1)
                    {
                        button1.BackColor = Color.Red;
                        button2.BackColor = Color.Red;
                        button3.BackColor = Color.Red;
                        button4.BackColor = Color.Red;
                        button5.BackColor = Color.Red;
                        button6.BackColor = Color.Red;
                        button7.BackColor = Color.Red;
                        button8.BackColor = Color.Red;
                    }
                    else if (sectionId == 2)
                    {
                        button18.BackColor = Color.Red;
                        button19.BackColor = Color.Red;
                        button20.BackColor = Color.Red;
                        button21.BackColor = Color.Red;
                        button22.BackColor = Color.Red;
                        button25.BackColor = Color.Red;
                        button28.BackColor = Color.Red;
                        button31.BackColor = Color.Red;
                    }
                    else if (sectionId == 3)
                    {
                        button34.BackColor = Color.Red;
                        button35.BackColor = Color.Red;
                        button36.BackColor = Color.Red;
                        button37.BackColor = Color.Red;
                        button38.BackColor = Color.Red;
                        button41.BackColor = Color.Red;
                        button44.BackColor = Color.Red;
                        button47.BackColor = Color.Red;
                    }
                    Instruction = "后方安全";
                }
                else if (Direction == Direction.Back)
                {
                    if (sectionId == 1)
                    {
                        button9.BackColor = Color.Red;
                        button10.BackColor = Color.Red;
                        button11.BackColor = Color.Red;
                        button12.BackColor = Color.Red;
                        button13.BackColor = Color.Red;
                        button14.BackColor = Color.Red;
                        button15.BackColor = Color.Red;
                        button16.BackColor = Color.Red;
                    }
                    else if (sectionId == 2)
                    {
                        button17.BackColor = Color.Red;
                        button23.BackColor = Color.Red;
                        button24.BackColor = Color.Red;
                        button26.BackColor = Color.Red;
                        button27.BackColor = Color.Red;
                        button29.BackColor = Color.Red;
                        button30.BackColor = Color.Red;
                        button32.BackColor = Color.Red;
                    }
                    else if (sectionId == 3)
                    {
                        button33.BackColor = Color.Red;
                        button39.BackColor = Color.Red;
                        button40.BackColor = Color.Red;
                        button42.BackColor = Color.Red;
                        button43.BackColor = Color.Red;
                        button45.BackColor = Color.Red;
                        button46.BackColor = Color.Red;
                        button48.BackColor = Color.Red;
                    }
                    Instruction = "前方安全";
                }
                else if (Direction == Direction.Left)
                {
                    if (sectionId == 1)
                    {
                        button1.BackColor = Color.Red;
                        button2.BackColor = Color.Red;
                        button5.BackColor = Color.Red;
                        button6.BackColor = Color.Red;
                        button9.BackColor = Color.Red;
                        button10.BackColor = Color.Red;
                        button13.BackColor = Color.Red;
                        button14.BackColor = Color.Red;
                    }
                    else if (sectionId == 2)
                    {
                        button18.BackColor = Color.Red;
                        button19.BackColor = Color.Red;
                        button22.BackColor = Color.Red;
                        button24.BackColor = Color.Red;
                        button25.BackColor = Color.Red;
                        button26.BackColor = Color.Red;
                        button30.BackColor = Color.Red;
                        button32.BackColor = Color.Red;
                    }
                    else if (sectionId == 3)
                    {
                        button34.BackColor = Color.Red;
                        button35.BackColor = Color.Red;
                        button38.BackColor = Color.Red;
                        button40.BackColor = Color.Red;
                        button41.BackColor = Color.Red;
                        button42.BackColor = Color.Red;
                        button46.BackColor = Color.Red;
                        button48.BackColor = Color.Red;
                    }
                    Instruction = "右侧安全";
                }
                else if (Direction == Direction.Right)
                {
                    if (sectionId == 1)
                    {
                        button3.BackColor = Color.Red;
                        button4.BackColor = Color.Red;
                        button7.BackColor = Color.Red;
                        button8.BackColor = Color.Red;
                        button11.BackColor = Color.Red;
                        button12.BackColor = Color.Red;
                        button15.BackColor = Color.Red;
                        button16.BackColor = Color.Red;
                    }
                    else if (sectionId == 2)
                    {
                        button17.BackColor = Color.Red;
                        button20.BackColor = Color.Red;
                        button21.BackColor = Color.Red;
                        button23.BackColor = Color.Red;
                        button27.BackColor = Color.Red;
                        button28.BackColor = Color.Red;
                        button29.BackColor = Color.Red;
                        button31.BackColor = Color.Red;
                    }
                    else if (sectionId == 3)
                    {
                        button33.BackColor = Color.Red;
                        button36.BackColor = Color.Red;
                        button37.BackColor = Color.Red;
                        button39.BackColor = Color.Red;
                        button43.BackColor = Color.Red;
                        button44.BackColor = Color.Red;
                        button45.BackColor = Color.Red;
                        button47.BackColor = Color.Red;
                    }
                    Instruction = "左侧安全";
                }
            } 

            return Instruction;
        }
        #endregion

        #region Event Handlers
        private void btnExe_Click(object sender, EventArgs e)
        {
            InitButtonPanel(); 
            InitPointer();

            label1.Text = GetActionsforRow(comboBox4, comboBox3, comboBox2, comboBox1, 1);
            label2.Text = GetActionsforRow(comboBox5, comboBox6, comboBox7, comboBox8, 1);
            label3.Text = GetActionsforRow(comboBox9, comboBox10, comboBox11, comboBox12, 1);
            label4.Text = GetActionsforRow(comboBox13, comboBox14, comboBox15, comboBox16, 1);

            label5.Text = GetActionsforRow(comboBox29, comboBox30, comboBox31, comboBox32, 2);
            label6.Text = GetActionsforRow(comboBox25, comboBox26, comboBox27, comboBox28, 2);
            label7.Text = GetActionsforRow(comboBox21, comboBox22, comboBox23, comboBox24, 2);
            label8.Text = GetActionsforRow(comboBox17, comboBox18, comboBox19, comboBox20, 2);
            
            label9.Text = GetActionsforRow(comboBox45, comboBox46, comboBox47, comboBox48, 3);
            label10.Text = GetActionsforRow(comboBox41, comboBox42, comboBox43, comboBox44, 3);
            label11.Text = GetActionsforRow(comboBox37, comboBox38, comboBox39, comboBox40, 3);
            label12.Text = GetActionsforRow(comboBox33, comboBox34, comboBox35, comboBox36, 3);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearComboBoxes();
            InitLabels();
            InitButtonPanel();
            InitPointer();
        }

        private void btnGroup_Enter(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
