using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfHomeWork6
{
    //Разработать в WPF приложении класс WeatherControl, моделирующий погодную сводку
    //– температуру(целое число в диапазоне от -50 до +50),
    //направление ветра(строка), скорость ветра(целое число),
    //наличие осадков(возможные значения: 0 – солнечно, 1 – облачно, 2 – дождь, 3 – снег.
    //Можно использовать целочисленное значение, либо создать перечисление enum).
    //Свойство «температура» преобразовать в свойство зависимости.
    public class WeatherControl : DependencyObject
    {
        public static readonly DependencyProperty TemperatureProperty;      //свойство зависимости температура
        public int Temperature      //св-во температура
        {
            get => (int)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }
        public WindDirection WindDirection { get; set; }        //автосвойство направление ветра
        public int WindSpeed { get; set; }      //автосвойство скорость ветра
        public Precipitation Precipitation { get; set; }        //автосвойство наличие осадков
        public WeatherControl(int temperature, WindDirection windDirection, int windSpeed, Precipitation precipitation)     //конструктор объекта WeatherControl
        {
            this.Temperature = temperature;
            this.WindDirection = windDirection;
            this.WindSpeed = windSpeed;
            this.Precipitation = precipitation;
        }
        static WeatherControl()     //статический конструктор в котором происходит инициализация свойства зависимости температура
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    new CoerceValueCallback(CoerceTemperature)),
                new ValidateValueCallback(ValidateTemperature));
        }

        private static bool ValidateTemperature(object value)       //метод валидации значения
        {
            int v = (int)value;
            if (v >= -50 && v <= 50)
                return true;
            else
                return false;
        }

        private static object CoerceTemperature(DependencyObject d, object baseValue)       //метод коррекции значения
        {
            int v = (int)baseValue;
            if (v < -50)
                v = -50;
            else if (v > 50)
                v = 50;
            return v;
        }
    }
    public enum WindDirection       //перечисление направлений ветра
    {
        North = 0,
        NorthEast = 1,
        NortWeast = 2,
        South = 3,
        SouthEast = 4,
        SouthWeast = 5,
        East = 6,
        West = 7
    }
    public enum Precipitation       //перечисление наличия осадков
    {
        Sunny = 0,
        Cloudy = 1,
        Rainy = 2,
        Snowy = 3
    }
}
