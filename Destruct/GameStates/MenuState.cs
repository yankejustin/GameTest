﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destruct.GameStates
{
    public class MenuState : GameState
    {
        int selected;
        bool isOldDown;
        string[] options = new string[] { "Single Player", "Host Game", "Join Game", "Exit" };
        GameStateManager gsm;
        bool isOldDownU;
        bool isOldDownD;
        bool isOldDownM;

        public MenuState(GameStateManager gsm)
        {
            this.gsm = gsm;
        }

        public override void Init()
        {
        }

        public override void Update()
        {
            for (int i = 0; i < options.Length; i++ )
            {
                if(new Rectangle(0, 200 + (i * 25),1000, 1000).Contains(Globals.mouseX, Globals.mouseY))
                    selected = i;
            }
            if (Utilities.NativeKeyboard.IsKeyDown(Utilities.KeyCode.Up) && !isOldDownU)
                if (selected > 0)
                    selected--;
                else
                    selected = options.Length;

            if (Utilities.NativeKeyboard.IsKeyDown(Utilities.KeyCode.Down) && !isOldDownD)
                if (selected < options.Length - 1)
                    selected++;
                else
                    selected = 0;
            if ((isOldDown && !Utilities.NativeKeyboard.IsKeyDown(Utilities.KeyCode.Space)) || (isOldDownM && !Utilities.NativeKeyboard.IsKeyDown(Utilities.KeyCode.LeftMouse)))
                Select();
            isOldDown = Utilities.NativeKeyboard.IsKeyDown(Utilities.KeyCode.Space);
            isOldDownM = Utilities.NativeKeyboard.IsKeyDown(Utilities.KeyCode.LeftMouse);
            isOldDownU = Utilities.NativeKeyboard.IsKeyDown(Utilities.KeyCode.Up);
            isOldDownD = Utilities.NativeKeyboard.IsKeyDown(Utilities.KeyCode.Down);
        }

        public void Select()
        {
            GameState state;
            switch(selected)
            {
                case 0:
                    gsm.currentSession = new MainState(this.gsm);
                    state = gsm.currentSession;
                    state.Init();
                    gsm.currentState = state;
                    
                    break;
                case 1:
                    state = new MultiState(true);
                    state.Init();
                    gsm.currentState = state;
                    break;
                case 2:
                    state = new MultiState(false);
                    state.Init();
                    gsm.currentState = state;
                    break;
                case 3:
                    Application.Exit();
                    break;
            }
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.FillRectangle(Brushes.brushBlack, 0, 0, Globals.screenSize, Globals.screenSize);
            for(int i = 0; i < options.Length; i++)
            {
                Color c = Color.SlateGray;
                if(selected == i)
                    c = Color.White;
                g.DrawString(options[i], new Font(FontFamily.GenericSansSerif, 20), new SolidBrush(c), new PointF(Globals.halfScreenSize, 200 + (i * 25)));
            }
        }
    }
}
