using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TvdbLib;

namespace EpisodeTitleParser
{
    public class TitleParser
    {
        private List<EpisodeFilter> _conventionStrings;
        public TitleParser() 
        {
            /* ************** Legend part episodes *********** *\
             * # - character placement (may or may not exist)  *
            \* *********************************************** */
            _conventionStrings = new List<EpisodeFilter>();
            _conventionStrings.AddRange(new EpisodeFilter[] { 
                // * multi part  episodes *
                // ex: s01#e01#e02
                  new EpisodeFilter(SESplit, new Regex("s[0-9]{1,}.?e[0-9]{1,}.?e[0-9]{1,}", RegexOptions.IgnoreCase))
                // ex: s01-e01-02-03
                , new EpisodeFilter(SESplit, new Regex("s[0-9]{1,}.?e[0-9]{1,}.[0-9]{1,}", RegexOptions.IgnoreCase))
                // ex: 1x01x02
                , new EpisodeFilter(XSplit, new Regex("[0-9]{1,}x[0-9]{1,}x[0-9]{1,}", RegexOptions.IgnoreCase))
                // ex: ep01#02
                , new EpisodeFilter(EPSplit, new Regex("ep[0-9]{1,}.[0-9]{1,}", RegexOptions.IgnoreCase))
                // ex: 010102
                , new EpisodeFilter(NumberSplit, new Regex("ep.[0-9]{3,}.?[0-9]{2,}", RegexOptions.IgnoreCase))                
                // * single part episodes and long naming *
                // ex: s01#e01
                , new EpisodeFilter(SESplit, new Regex("s[0-9]{1,}.?e[0-9]{1,}", RegexOptions.IgnoreCase))
                // ex: 1x01
                , new EpisodeFilter(XSplit, new Regex("[0-9]{1,}x[0-9]{1,}", RegexOptions.IgnoreCase))
                // ex: ep01
                , new EpisodeFilter(EPSplit, new Regex("ep[0-9]{1,}", RegexOptions.IgnoreCase))
                // ex: 1014 or 101
                , new EpisodeFilter(NumberSplit, new Regex("[0-9]{3,}", RegexOptions.IgnoreCase))
            });


        }

        public EpisodeInfo GetEpisodeInfo(string originalTitle)
        {
            string assumedName;
            return new EpisodeInfo(
                StripEpisodeParameters(originalTitle, out assumedName),
                GetEpisodeWords(originalTitle),
                assumedName
                );
        }

        private List<string> GetEpisodeWords(string originalTitle)
        {
            List<string> ret = new List<string>();
            foreach (string s in Regex.Replace(originalTitle, "[^a-zA-Z]", " ").Trim().Split(' '))
            {
                if (s.Equals(String.Empty))
                    continue;
                ret.Add(s);
            }
           
            return ret;
        }

        #region Series Order
        private EpisodeSplit StripEpisodeParameters(string originalTitle, out string assumedName)
        {
            string episodeParams = string.Empty;
            foreach (EpisodeFilter ep in _conventionStrings)
                if (ep.StripInfo(originalTitle, out assumedName))
                    return ep.EpisodeParams;

            assumedName = null;
            return null;
        }

        #region parameterStrip
        private string SESplit(string match)
        {
            StringBuilder param = new StringBuilder(); 
            Match mat = Regex.Match(match, "s[0-9]{1,}", RegexOptions.IgnoreCase);
            param.Append(mat.Value.ToLower().Replace("s", "")).Append("|");

            foreach (Match matE in Regex.Matches(match, "E.?[0-9]{1,}", RegexOptions.IgnoreCase))
            {
                string episodeNum = Regex.Replace(matE.Value, "[^0-9]", "", RegexOptions.IgnoreCase);
                param.Append(episodeNum).Append("|");
            }
            return param.ToString().TrimEnd('|');
        }

        private string XSplit(string match)
        {
            StringBuilder param = new StringBuilder();
            param.Append(Regex.Match(match, "[0-9]{1,}x", RegexOptions.IgnoreCase)
                    .Value
                    .ToLower()
                    .Replace("x", ""))
                .Append("|");

            foreach (Match mat in Regex.Matches(match, "x.?[0-9]{1,}", RegexOptions.IgnoreCase))
            {
                string episodeNum = Regex.Replace(mat.Value, "[^0-9]", "", RegexOptions.IgnoreCase);
                param.Append(episodeNum).Append("|");
            }
            return param.ToString().TrimEnd('|');
        }

        private string EPSplit(string match)
        {
            StringBuilder param = new StringBuilder();

            param.Append("00|");
            foreach (Match mat in Regex.Matches(match, "ep[0-9]{1,}", RegexOptions.IgnoreCase))
                param.Append(mat.Value.ToLower().Replace("ep", string.Empty).Trim()).Append("|");

            return param.ToString().TrimEnd('|');
        }

        private string NumberSplit(string match)
        {
            StringBuilder param = new StringBuilder();
            if (match.Length > 3)
            {
                if (match.Length % 2 == 0)
                {
                    for (int i = 0; i < match.Length; i += 2)
                        param.Append(match.Substring(i, 2)).Append("|");
                }
                else
                {
                    param.Append(int.Parse(match.Substring(0, 1)).ToString("00")).Append("|");
                    for (int i = 1; i < match.Length; i += 2)
                        param.Append(match.Substring(i, 2)).Append("|");
                }
            }
            else
            {
                if (match.Length == 3)
                    param.Append(match.Substring(0, 1))
                        .Append("|")
                        .Append(match.Substring(1, 2));
                else
                    param.Append(match.Substring(0, 1))
                        .Append("|")
                        .Append(match.Substring(1, 1));
            }

            return param.ToString().TrimEnd('|');
        }
        #endregion
        #endregion
    }

    #region Support Classes
    public class EpisodeInfo
    {
        public EpisodeSplit SeriesOrder{ get; private set;}
        public List<string> EpisodeWords;
        public string AssumedName;
        public EpisodeInfo(EpisodeSplit es, List<string> ls, string s)
        {
            SeriesOrder = es;
            EpisodeWords = ls;
            AssumedName = s;
        }
    }

    public class EpisodeSplit
    {
        public string season;
        public string[] episodes;

        public EpisodeSplit(string[] parameters)
        {
            if (parameters.Length > 1)
            {
                season = parameters[0];
                episodes = new string[parameters.Length - 1];
                Array.ConstrainedCopy(parameters, 1, episodes, 0, parameters.Length - 1);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(int.Parse(season).ToString("00")).Append("|");
            foreach (string s in episodes)
                sb.Append(int.Parse(s).ToString("00")).Append("|");

            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }

    public class EpisodeFilter
    {
        private Func<string, string> _stripMethod;
        private Regex _expression;
        public EpisodeSplit EpisodeParams { get; private set; }
        public EpisodeFilter(Func<string, string> stripMethod, Regex match)
        {
            _stripMethod = stripMethod;
            _expression = match;
        }
        public bool StripInfo(string episode, out string assumedName)
        {
            if (_expression.IsMatch(episode))
            {
                string match = _expression.Match(episode).Value;
                string temp = episode.Substring(0, episode.IndexOf(match));
                assumedName = Regex.Replace(temp, "[^a-z0-9]", " ", RegexOptions.IgnoreCase).ToLower();
                string[] param = _stripMethod(match).Split('|');
                EpisodeParams = new EpisodeSplit(param);
                return true;
            }
            assumedName = null;
            return false;
        }
    }
    #endregion

    public class EpisodeSeriesBuilder
    {
        private static string _sAPIKey = "3B7D64677D2F8319";
        private static Dictionary<char, List<string>> _filterwords;
        private static Dictionary<string, KeyValuePair<int, bool>> _dsiSeriesMemory;

        public EpisodeSeriesBuilder()
        {
            _dsiSeriesMemory = new Dictionary<string, KeyValuePair<int, bool>>();

            #region populate words to filter
            _filterwords = new Dictionary<char, List<string>>();
            _filterwords.Add('a', new List<string>(
                new string[] { "A", "Adventures", "All", "America", "American", "And", "Angel", "At" }));
            _filterwords.Add('b', new List<string>(
                new string[] { "Big", "Blue", "Bunny", "By" }));
            _filterwords.Add('c', new List<string>(
                new string[] { "Captain", "Challenge", "City", "Club", "Comedy" }));
            _filterwords.Add('d', new List<string>(
                new string[] { "Dark", "Day", "Days", "Dead", "Dog", "Dr" }));
            _filterwords.Add('f', new List<string>(
                new string[] { "Family", "Fat", "For", "Friends", "From" }));
            _filterwords.Add('g', new List<string>(
                new string[] { "Game", "Get", "Ghost", "Girl", "Girls", "Good", "Green", "Guy" }));
            _filterwords.Add('h', new List<string>(
                new string[] { "High", "Home", "Hour", "House" }));
            _filterwords.Add('i', new List<string>(
                new string[] { "I", "In", "Is", "Island", "It", "It's" }));
            _filterwords.Add('k', new List<string>(
                new string[] { "Kids", "King" }));
            _filterwords.Add('l', new List<string>(
                new string[] { "Late", "Law", "Legend", "Life", "Little", "Live", "Lost", "Love" }));
            _filterwords.Add('m', new List<string>(
                new string[] { "Man", "Masters", "Me", "Men", "Mr", "Murder", "My" }));
            _filterwords.Add('n', new List<string>(
                new string[] { "New", "Next", "Night", "North" }));
            _filterwords.Add('o', new List<string>(
                new string[] { "Of", "On", "One", "Or", "Out" }));
            _filterwords.Add('p', new List<string>(
                new string[] { "Place", "Police", "Power", "Presents" }));
            _filterwords.Add('r', new List<string>(
                new string[] { "Rangers", "Real", "Red", "Road", "Rock", "Rules" }));
            _filterwords.Add('s', new List<string>(
                new string[] { "School", "Science", "Secret", "Series", "Show", "Sons", "South", "Space", "Star", "Street", "Super" }));
            _filterwords.Add('t', new List<string>(
                new string[] { "Tales", "That", "The", "Time", "To", "Tom", "Tonight", "TV", "Two" }));
            _filterwords.Add('u', new List<string>(
                new string[] { "Universe", "Up" }));
            _filterwords.Add('v', new List<string>(
                new string[] { "Vs" }));
            _filterwords.Add('w', new List<string>(
                new string[] { "Wars", "West", "What", "Whose", "Wild", "With", "World" }));
            _filterwords.Add('y', new List<string>(
                new string[] { "You", "Young", "Your" }));
            #endregion
        }

        public Dictionary<string, List<string>> SeriesEpisodes(string sSeries, 
            Func<List<TvdbLib.Data.TvdbSeries>, int, KeyValuePair<int, bool>> SeriesSelectFunc)
        {
            List<TvdbLib.Data.TvdbSeries> lTvSeries = EpisodeSeriesBuilder.GetSeriesFromTitle(sSeries);
            Dictionary<string, List<string>> episodesReturn = new Dictionary<string,List<string>>();

            if (lTvSeries.Count <= 0)
                return null;

            TvdbLib.Data.TvdbSeries TvS = SeriesSearchSelectList(lTvSeries, sSeries, SeriesSelectFunc);

            for (int i = 1; i <= TvS.NumSeasons; i++)
            {
                List<TvdbLib.Data.TvdbEpisode> lTvE = TvS.GetEpisodes(i);
                foreach (TvdbLib.Data.TvdbEpisode TvE in lTvE)
                {
                    string season = string.Format("Season {0}", i);
                    if (!episodesReturn.ContainsKey(season))
                        episodesReturn.Add(season,
                            new List<string>(new string[] { TvE.EpisodeNumber.ToString() + " : " + TvE.EpisodeName }));
                    else
                        episodesReturn[season].Add(TvE.EpisodeNumber.ToString() + " : " + TvE.EpisodeName);
                }
            }

            if (episodesReturn.Count <= 0)
                episodesReturn = ContructEpisodeList(TvS);

            return episodesReturn;
        }

        public string EpisodeNameReturn(EpisodeInfo ei, 
            Func<List<TvdbLib.Data.TvdbSeries>, int, KeyValuePair<int, bool>> SeriesSelectFunc)
        {
            TvdbLib.Data.TvdbSeries TvSeries = 
                EpisodeSeriesBuilder.GetSeriesFromSearch(ei, SeriesSelectFunc);

            if (TvSeries == null)
                return "NOTFOUND";

            if (ei.SeriesOrder == null)
                return "NOTFOUND";

            int season = int.Parse(ei.SeriesOrder.season);

            List<TvdbLib.Data.TvdbEpisode> lTvE = TvSeries.GetEpisodes(season);

            if (lTvE.Count <= 0)
                return "NOTFOUND";

            int iIndex;
            if ((iIndex = lTvE.FindIndex(delegate(TvdbLib.Data.TvdbEpisode t)
            {
                if (t.EpisodeNumber == int.Parse(ei.SeriesOrder.episodes[0]))
                    return true;
                return false;
            })) != -1)
                return string.Format("{0} S{1}E{2} - {3}"
                        , TvSeries.SeriesName
                        , season > 99 ? season.ToString("000") : season.ToString("00")
                        , int.Parse(ei.SeriesOrder.episodes[0]) > 99 ?
                            int.Parse(ei.SeriesOrder.episodes[0]).ToString("000") :
                            int.Parse(ei.SeriesOrder.episodes[0]).ToString("00")
                        , lTvE[iIndex].EpisodeName);
            else
                return "NOTFOUND";
        }

        public static Dictionary<string, List<string>> ContructEpisodeList(TvdbLib.Data.TvdbSeries tvsSeries)
        {
            Dictionary<string, List<string>> episodesReturn = new Dictionary<string, List<string>>();
            if (tvsSeries == null)
                return null;
            List<TvdbLib.Data.TvdbEpisode> episodes = tvsSeries.GetEpisodesAbsoluteOrder();

            foreach (TvdbLib.Data.TvdbEpisode episode in episodes)
                if (episode.FirstAired.CompareTo(tvsSeries.FirstAired) < 1)
                    episode.FirstAired = tvsSeries.FirstAired;

            episodes.Sort(new OrderByDate().Compare);

            foreach (TvdbLib.Data.TvdbEpisode episode in episodes)
            {
                string season = episode.FirstAired.Year.ToString("0000");

                if (!episodesReturn.ContainsKey(season))
                    episodesReturn.Add(season,
                        new List<string>(new string[] { episode.EpisodeName }));
                else
                    episodesReturn[season].Add(episode.EpisodeName);
            }

            return episodesReturn;
        }

        public static TvdbLib.Data.TvdbSeries GetSeriesFromSearch(EpisodeInfo ei,
            Func<List<TvdbLib.Data.TvdbSeries>, int, KeyValuePair<int, bool>> SeriesSelectFunc)
        {
            List<TvdbLib.Data.TvdbSeries> lTvSeries = null;
            StringBuilder titleBuild = new StringBuilder();

            #region parsed name method
            if (ei.AssumedName != null)
            {
                string temp = "";
                foreach (string s in ei.AssumedName.Trim().Split(' '))
                {
                    if (s.Trim().Length > 1)
                        temp += s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1) + " ";
                    else if (s.Trim().Length > 0)
                        temp += s.ToUpper() + " ";
                }
                ei.AssumedName = temp.Trim();

                lTvSeries = EpisodeSeriesBuilder.GetSeriesFromTitle(ei.AssumedName);
                if (lTvSeries != null && lTvSeries.Count < 20 && lTvSeries.Count > 0)
                    return SeriesSearchSelectList(lTvSeries, ei.AssumedName, SeriesSelectFunc);
            }
            #endregion

            #region swift search filter
            for (int index = 0; index < ei.EpisodeWords.Count; index++)
            {
                if (!_filterwords.ContainsKey(ei.EpisodeWords[index].ToLower()[0]))
                {
                    titleBuild.Append(ei.EpisodeWords[index]);
                    break;
                }

                string state = titleBuild.ToString();
                foreach (string s in _filterwords[ei.EpisodeWords[index].ToLower()[0]])
                    if (Regex.IsMatch(ei.EpisodeWords[index], s, RegexOptions.IgnoreCase))
                    {
                        titleBuild.Append(ei.EpisodeWords[index]).Append(" ");
                        break;
                    }

                if (state.Equals(titleBuild.ToString()))
                {
                    titleBuild.Append(ei.EpisodeWords[index]);
                    break;
                }
            }

            lTvSeries = EpisodeSeriesBuilder.GetSeriesFromTitle(titleBuild.ToString().Trim());
            if (!lTvSeries.Equals(null) && lTvSeries.Count > 0)
                return SeriesSearchSelectList(lTvSeries, titleBuild.ToString(), SeriesSelectFunc);
            #endregion

            #region final fallback
            titleBuild = new StringBuilder();
            for (int i = 0; i < ei.EpisodeWords.Count; i++)
            {
                titleBuild.Append(ei.EpisodeWords[i]).Append(" ");
                lTvSeries = EpisodeSeriesBuilder.GetSeriesFromTitle(titleBuild.ToString().Trim());

                if (lTvSeries != null && lTvSeries.Count <= 20)
                    return SeriesSearchSelectList(lTvSeries, titleBuild.ToString(), SeriesSelectFunc);
            }
            #endregion
            return null;   
        }

        public static int[] GetSeriesIDsFromTitle(string title)
        {

            List<TvdbLib.Data.TvdbSearchResult> lTvSeries = new TvdbHandler(_sAPIKey).SearchSeries(title,
                    TvdbLib.Data.TvdbLanguage.DefaultLanguage);
            
            int[] seriesIDs = new int[lTvSeries.Count > 0 ? lTvSeries.Count : 1];
            if (lTvSeries.Count <= 0)
                seriesIDs[0] = -1;

            for (int i = 0; i < lTvSeries.Count; i++)
                seriesIDs[i] = lTvSeries[i].Id;

            return seriesIDs;
        }

        public static List<TvdbLib.Data.TvdbSeries> GetSeriesFromTitle(string title)
        {
            List<TvdbLib.Data.TvdbSeries> lsOut = new List<TvdbLib.Data.TvdbSeries>();
            try
            {
                foreach (int id in GetSeriesIDsFromTitle(title))
                    if (!id.Equals(-1))
                        lsOut.Add(new TvdbHandler(_sAPIKey).GetFullSeries(id
                            , TvdbLib.Data.TvdbLanguage.DefaultLanguage
                            , false
                        ));
            }
            catch (TvdbLib.Exceptions.TvdbNotAvailableException ex)
            {

            }
            return lsOut;
        }

        private static TvdbLib.Data.TvdbSeries SeriesSearchSelectList(List<TvdbLib.Data.TvdbSeries> lTvSeries, string sSeries,
            Func<List<TvdbLib.Data.TvdbSeries>, int, KeyValuePair<int, bool>> SeriesSelectFunc)
        {
            if (lTvSeries.Count.Equals(1))
                return new TvdbHandler(_sAPIKey).GetFullSeries(lTvSeries[0].Id
                    , TvdbLib.Data.TvdbLanguage.DefaultLanguage
                    , false);

            if (!_dsiSeriesMemory.ContainsKey(sSeries))
                _dsiSeriesMemory.Add(sSeries, new KeyValuePair<int, bool>(-1, false));

            if (!_dsiSeriesMemory[sSeries].Value)
                _dsiSeriesMemory[sSeries] = SeriesSelectFunc(lTvSeries, _dsiSeriesMemory[sSeries].Key);

            return new TvdbHandler(_sAPIKey).GetFullSeries(_dsiSeriesMemory[sSeries].Key
                , TvdbLib.Data.TvdbLanguage.DefaultLanguage
                , false);
        }
    }
    public class OrderByDate : IComparer<TvdbLib.Data.TvdbEpisode>
    {
        public int Compare(TvdbLib.Data.TvdbEpisode e1, TvdbLib.Data.TvdbEpisode e2)
        {
            if (e1.FirstAired == null) return -1;
            if (e2.FirstAired == null) return 1;

            if (e1.FirstAired.Year > e2.FirstAired.Year) return 1;
            else if (e1.FirstAired.Year < e2.FirstAired.Year) return -1;
            else
            {
                if (e1.FirstAired.Month > e2.FirstAired.Month) return 1;
                else if (e1.FirstAired.Month < e2.FirstAired.Month) return -1;
                else
                {
                    if (e1.FirstAired.Day > e2.FirstAired.Day) return 1;
                    else if (e1.FirstAired.Day < e2.FirstAired.Day) return -1;
                    else return 0;
                }
            }
        }
    }
}
