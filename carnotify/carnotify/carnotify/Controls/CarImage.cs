using System.Threading.Tasks;
using Xamarin.Forms;

namespace carnotify.Controls
{
    public class CarImage : Image
    {
        public static readonly BindableProperty IsDrivingProperty = BindableProperty.Create("IsDriving", typeof(bool), typeof(CarImage), false, propertyChanged: OnDriveChanged);

        public bool IsDriving { get; set; }

        static void OnDriveChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var oldVal = (bool)oldValue;
            var newVal = (bool)newValue;

            var carImage = (CarImage)bindable;

            const int animationSpeed = 800;

            if (!oldVal && newVal)
            {
                Task.Run(async () =>
                {
                    await carImage.TranslateTo(-800, 0, animationSpeed);
                    await carImage.FadeTo(0, 0);
                }).ConfigureAwait(false);
            }
            else if (oldVal && !newVal)
            {
                Task.Run(async () =>
                {
                    await carImage.TranslateTo(800, 0, 0);
                    await carImage.FadeTo(1, 0);
                    await carImage.TranslateTo(0, 0, animationSpeed);
                }).ConfigureAwait(false);
            }
        }
    }
}
