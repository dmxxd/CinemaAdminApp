using System.Collections.Generic;
using System.Linq;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Logic
{
    public class FilmManager
    {
        public List<Film> GetAllFilms() => DataStorage.GetAllFilms();

        public Film GetFilmById(int id) => DataStorage.GetFilmById(id);

        public bool AddFilm(Film film) => DataStorage.AddFilm(film);

        public bool UpdateFilm(Film film) => DataStorage.UpdateFilm(film);

        public bool DeleteFilm(int id) => DataStorage.DeleteFilm(id);
    }
}
