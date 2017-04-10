using System.Collections;

public static class Urls
{
	static string base_url = "http://wealthynator.justaflstuff.com/";

	public static string players_for_team (string team_id)
	{
		return base_url + "teams/" + team_id + "/players.json";
	}

	public static string player_stats (string player_id)
	{
		return base_url + "player_stats/" + player_id + "/all_stats?opposition_id=all";
	}
}
