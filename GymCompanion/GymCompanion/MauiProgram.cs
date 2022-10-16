using CommunityToolkit.Maui;
using GymCompanion.ViewModels.BodyParts;
using GymCompanion.ViewModels.Exercises;
using GymCompanion.Views.Exercises;
using GymCompanion.Views.BodyParts;

namespace GymCompanion;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

        builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        builder.Services.AddSingleton<BodyPartCalls>();
        builder.Services.AddSingleton<ExerciseCalls>();

        builder.Services.AddSingleton<BodyPartsListViewModel>();
        builder.Services.AddTransient<BodyPartDetailsViewModel>();
        builder.Services.AddTransient<BodyPartCreateViewModel>();

        builder.Services.AddSingleton<ExercisesListViewModel>();
        builder.Services.AddTransient<ExerciseDetailsViewModel>();
        builder.Services.AddTransient<ExerciseCreateViewModel>();

        builder.Services.AddSingleton<BodyPartsList>();
		builder.Services.AddTransient<BodyPartDetailsPage>();
        builder.Services.AddTransient<BodyPartCreatePage>();

        builder.Services.AddSingleton<ExercisesList>();
        builder.Services.AddTransient<ExerciseDetailsPage>();
        builder.Services.AddTransient<ExerciseCreatePage>();

        return builder.Build();
	}
}
