using System.Windows.Media;


namespace OrdiniCatCe.Gui.Constants
{
    public class AppColors
    {
        private const byte _alpha = 50;

        public static Color NextActionColor;
        public static Color EnabledButtonColor;


        //ROW BACKGROUND
        public static Color OrdinatoRowBackground;
        public static Color AvvisatoRowBackground;
        public static Color ArrivatoRowBackground;
        public static Color RitiratoRowBackground;

        public static Color HighlightProblemBackColor;

        static AppColors()
        {
            OrdinatoRowBackground = Colors.Orange;
            OrdinatoRowBackground.A = _alpha;

            AvvisatoRowBackground = Colors.LightGreen;
            AvvisatoRowBackground.A = _alpha;

            ArrivatoRowBackground = Colors.Red;
            ArrivatoRowBackground.A = _alpha;

            RitiratoRowBackground = Colors.Turquoise;
            RitiratoRowBackground.A = _alpha;

            NextActionColor = Colors.MediumSpringGreen;
            NextActionColor.A = _alpha;

            EnabledButtonColor = Colors.MediumAquamarine;
            EnabledButtonColor.A = _alpha;

            HighlightProblemBackColor = Color.FromArgb(125, 255, 150, 10);
        }
    }
}
