
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using System.IO;
using System.Configuration;

namespace TrufinIS_L3
{
    class Triangle : GameWindow
    {
        private Vector3[] triangleVertices;
        private Color4[] vertexColours;

        private float rotationX, rotationY;
        private bool isMousePressed = false;
        private int selectedVertex = -1;
        private KeyboardState previousKeyboard;

        private const float MIN_VAL = 0.0f;
        private const float MAX_VAL = 1.0f;
        private const float VAL = 0.05f;
        private Color DEFAULT_BACK_COLOR = Color.Beige;

        public Triangle() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            Location = new Point(
        (DisplayDevice.Default.Width - Width) / 2,
        (DisplayDevice.Default.Height - Height) / 2
    );
            InitializeTriangle();
            Help();
        }

        private void InitializeTriangle()
        {
            // coordonate  din fișier
            string filepath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\" +
                ConfigurationManager.AppSettings["NumeFisier"];
            triangleVertices = ReadTriangleCoord(filepath);
            if (triangleVertices == null || triangleVertices.Length < 3)
            {
                Console.WriteLine("Coordonatele triunghiului nu au fost incarcate corect.");
                Exit();
            }



            vertexColours = new Color4[3]
            {
                new Color4(0.0f, 0.0f, 0.0f, 1.0f), // Varful 1
                new Color4(0.0f, 0.0f, 0.0f, 1.0f), // Varful 2
                new Color4(0.0f, 0.0f, 0.0f, 1.0f)  // Varful 3
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.ClearColor(DEFAULT_BACK_COLOR);
            GL.Viewport(0, 0, this.Width, this.Height);

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 250);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 eye = Matrix4.LookAt(0, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref eye);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();

            if (currentKeyboard[Key.Escape])
                Exit();

            if (currentKeyboard[Key.M] && !previousKeyboard[Key.M])
                Help();

            // Modificarea culorii
            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R]) ModifyVertexColor("R", selectedVertex);
            if (currentKeyboard[Key.G] && !previousKeyboard[Key.G]) ModifyVertexColor("G", selectedVertex);
            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B]) ModifyVertexColor("B", selectedVertex);
            if (currentKeyboard[Key.A] && !previousKeyboard[Key.A]) ModifyVertexColor("A", selectedVertex);
            if (currentKeyboard[Key.D] && !previousKeyboard[Key.A]) ModifyVertexColor("D", selectedVertex);

            if (currentKeyboard[Key.X] && !previousKeyboard[Key.X])
                ResetVertexColors();

            if (currentKeyboard[Key.Number1]) selectedVertex = 0;
            if (currentKeyboard[Key.Number2]) selectedVertex = 1;
            if (currentKeyboard[Key.Number3]) selectedVertex = 2;

            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
                DisplayVertexColors();

            isMousePressed = currentMouse.IsButtonDown(MouseButton.Left);
            previousKeyboard = currentKeyboard;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (isMousePressed)
            {
                GL.Rotate(rotationX, 0.0f, 1.0f, 0.0f);
                GL.Rotate(rotationY, 1.0f, 0.0f, 0.0f);
            }

            DrawTriangle();
            SwapBuffers();
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (isMousePressed)
            {
                rotationX += e.XDelta * 0.005f;
                rotationY += e.YDelta * 0.005f;
            }
        }

        private void DrawTriangle()
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Begin(PrimitiveType.Triangles);

            for (int i = 0; i < triangleVertices.Length; i++)
            {
                GL.Color4(vertexColours[i]);
                GL.Vertex3(triangleVertices[i]);
            }

            GL.End();
        }


        private void ModifyVertexColor(string colorChannel, int vertexIndex = -1)
        {
            if (vertexIndex >= 0 && vertexIndex < vertexColours.Length)
            {
                switch (colorChannel)
                {
                    case "R":
                        vertexColours[vertexIndex].R = MathHelper.Clamp(vertexColours[vertexIndex].R + VAL, MIN_VAL, MAX_VAL);
                        break;
                    case "G":
                        vertexColours[vertexIndex].G = MathHelper.Clamp(vertexColours[vertexIndex].G + VAL, MIN_VAL, MAX_VAL);
                        break;
                    case "B":
                        vertexColours[vertexIndex].B = MathHelper.Clamp(vertexColours[vertexIndex].B + VAL, MIN_VAL, MAX_VAL);
                        break;
                    case "A":
                        vertexColours[vertexIndex].A = MathHelper.Clamp(vertexColours[vertexIndex].A - VAL, MIN_VAL, MAX_VAL);
                        break;
                    case "D":
                        vertexColours[vertexIndex].A = MathHelper.Clamp(vertexColours[vertexIndex].A + VAL, MIN_VAL, MAX_VAL);
                        break;
                }
            }
        }


        private void ResetVertexColors()
        {
            for (int i = 0; i < vertexColours.Length; i++)
            {
                vertexColours[i].R = vertexColours[i].G = vertexColours[i].B = 0.0f;
                vertexColours[i].A = 1.0f;
            }
        }

        private void DisplayVertexColors()
        {
            Console.WriteLine("\nCulori varfuri:");
            for (int i = 0; i < vertexColours.Length; i++)
            {
                Console.WriteLine($"Varf {i + 1} [ R: {vertexColours[i].R}, G: {vertexColours[i].G}, B: {vertexColours[i].B}, A: {vertexColours[i].A}]");
            }
        }

        private Vector3[] ReadTriangleCoord(string fileName)
        {
            Vector3[] points = new Vector3[3];

            try
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        string line = streamReader.ReadLine();
                        if (line != null)
                        {
                            string[] values = line.Split(',');
                            points[i] = new Vector3(
                                float.Parse(values[0]),
                                float.Parse(values[1]),
                                float.Parse(values[2])
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eroare la citirea fisierului: " + ex.Message);
            }

            return points;
        }

        private void Help()
        {
            Console.WriteLine("\nDetalii pentru folosirea aplicatiei:\nPentru schimbarea culorilor varfurilor se va selecta numarul varfului ( 1,2 sau 3), urmat de optiunea aferenta culorii dorite (R,G,B).\nSchimbarea culorii se face treptat prin apasarea repetata a tastei.\nSe urmeaza aceiasi pasi si pentru transparenta.");
            Console.WriteLine("\n   ALEGETI OPTIUNEA");

            Console.WriteLine(" R - Rosu");
            Console.WriteLine(" G - Verde");
            Console.WriteLine(" B - Albastru");
            Console.WriteLine(" A - Mareste transparenta");
            Console.WriteLine(" D - Micsoreaza transparenta ");
            Console.WriteLine(" X - Resetează RGBA");
            Console.WriteLine(" V - Valorile RGBA");
            Console.WriteLine(" M - Afisare meniu");
            Console.WriteLine(" ESC - Iesire");
            Console.WriteLine(" CLICK(stanga) - Rotire");

        }

        private void DrawAxes()
        {

            //GL.LineWidth(3.0f);

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(70, 0, 0);
            GL.End();


            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 70, 0); ;
            GL.End();


            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 70);
            GL.End();
        }
    }

}
