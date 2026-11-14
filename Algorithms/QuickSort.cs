
using CinemaFind.Models;

namespace CinemaFind.Algorithms
{
    public static class QuickSort
    {
        public static void Sort(List<Film> films)
        {
            if (films == null || films.Count <= 1)
                return;

            Sort(films, 0, films.Count - 1);
        }

        private static void Sort(
            List<Film> films,
            int low,
            int high)
        {
            if (low >= high)
                return;

            int pivotIndex = Partition(films, low, high);

            Sort(films, low, pivotIndex - 1);
            Sort(films, pivotIndex + 1, high);
        }

        private static int Partition(
            List<Film> films,
            int low,
            int high)
        {
            string pivot = films[high].Title;

            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (string.Compare(
                    films[j].Title,
                    pivot,
                    StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    i++;

                    (films[i], films[j]) =
                        (films[j], films[i]);
                }
            }

            (films[i + 1], films[high]) =
                (films[high], films[i + 1]);

            return i + 1;
        }
    }
}
