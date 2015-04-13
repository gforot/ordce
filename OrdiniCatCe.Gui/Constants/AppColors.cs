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

        public static Color ArrivatoButNotAvvisatoColor;
        public static Color OrdinatoButNotArrivatoColor;
        public static Color NotOrdinatoColor;

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

            HighlightProblemBackColor = Colors.RosyBrown;
            HighlightProblemBackColor.A = 125;
            ArrivatoButNotAvvisatoColor = Colors.Orange;
            ArrivatoButNotAvvisatoColor.A = 125;
            OrdinatoButNotArrivatoColor = Colors.Yellow;
            OrdinatoButNotArrivatoColor.A = 125;
            NotOrdinatoColor = Colors.Red;
            NotOrdinatoColor.A = 125;
        }
    }
}
