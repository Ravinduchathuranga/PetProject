﻿namespace PetProject
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();
            MainPage = mainPage;
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
