using System;

namespace P02.Graphic_Editor
{
    class Program
    {
        static void Main()
        {
            IShape square = new Square();
            IShape circle = new Circle();
            GraphicEditor editor = new GraphicEditor();

            editor.DrawShape(circle);
        }
    }
}
