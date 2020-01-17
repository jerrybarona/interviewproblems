using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Amazon
{
    public class P011FavoriteGenres
    {
        // https://leetcode.com/discuss/interview-question/344650/Amazon-Online-Assessment-Questions

        public Dictionary<string, List<string>> FavoriteGenres(Dictionary<string, List<string>> userSongs,
            Dictionary<string, List<string>> songGenres)
        {
            var songGenreMap = songGenres.Aggregate(new Dictionary<string,string>(), (dict, item) =>
            {
                foreach (var song in item.Value) dict.Add(song, item.Key);
                return dict;
            });

            return userSongs.ToDictionary(item => item.Key, item => GetUserGenres(item.Key).ToList());

            IEnumerable<string> GetUserGenres(string user)
            {
                var songs = userSongs[user];
                var map = songs.Aggregate(new Dictionary<string, int>(), (dict, song) =>
                {
                    var genre = songGenreMap[song];
                    if (!dict.ContainsKey(genre)) dict.Add(genre, 0);
                    ++dict[genre];
                    return dict;
                });

                var max = map.Select(item => item.Value).Max();
                return map.Where(item => item.Value == max).Select(x => x.Key);
            }
        }
    }
}
