using GymCompanion.Views.Exercises;
using GymCompanion.Views.BodyParts;

namespace GymCompanion;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        
        Routing.RegisterRoute(nameof(BodyPartDetailsPage), typeof(BodyPartDetailsPage));
        Routing.RegisterRoute(nameof(BodyPartCreatePage), typeof(BodyPartCreatePage));

        Routing.RegisterRoute(nameof(ExerciseCreatePage), typeof(ExerciseCreatePage));
        Routing.RegisterRoute(nameof(ExerciseDetailsPage), typeof(ExerciseDetailsPage));
    }
}
