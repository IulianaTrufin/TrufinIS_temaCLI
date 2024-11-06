//TRUFIN IULIANA SABINA
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace TrufinIS_L2
{
    class SimpleWindow3D : GameWindow
    {
        const float rotation_speed = 180.0f;
        float angleX = 0.0f;
        float angleY = 0.0f;
        float moveSpeed = 0.1f;


        float cubePosZ = 0.0f;


        float lastMouseX, lastMouseY;

        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
            lastMouseX = Width / 2;
            lastMouseY = Height / 2;
            var screenWidth = DisplayDevice.Default.Width;
            var screenHeight = DisplayDevice.Default.Height;
            Location = new Point((screenWidth - Width) / 2, (screenHeight - Height) / 2);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Olive);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 100);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();


            if (keyboard[OpenTK.Input.Key.A])
            {
                cubePosZ -= moveSpeed;
            }
            if (keyboard[OpenTK.Input.Key.S])
            {
                cubePosZ += moveSpeed;
            }


            float deltaX = mouse.X - lastMouseX;
            float deltaY = mouse.Y - lastMouseY;

            angleY += deltaX * 0.1f;
            angleX -= deltaY * 0.1f;


            lastMouseX = mouse.X;
            lastMouseY = mouse.Y;


            if (keyboard[OpenTK.Input.Key.Escape])
            {
                Exit();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            Matrix4 lookat = Matrix4.LookAt(0, 0, 10, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Translate(0.0f, 0.0f, cubePosZ);

            GL.Rotate(angleX, 1.0f, 0.0f, 0.0f); // Rotație pe axa X
            GL.Rotate(angleY, 0.0f, 1.0f, 0.0f); // Rotație pe axa Y


            DrawCube();

            SwapBuffers();
        }


        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.MistyRose);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.BurlyWood);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Peru);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.RosyBrown);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.DarkRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.Goldenrod);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow3D example = new SimpleWindow3D())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}