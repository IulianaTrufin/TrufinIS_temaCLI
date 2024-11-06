using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrufinIS_L4
{
    /// <summary>
    /// The main entry point for the application. This class initializes and runs the 3D rendering window.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main method, which serves as the entry point for the application.
        /// Initializes a new instance of <see cref="Window3D"/> and starts the rendering loop.
        /// </summary>
        /// <param name="args">Command-line arguments (not used in this application).</param>
        static void Main(string[] args)
        {
            using (Window3D example = new Window3D())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
