WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

int userScore = 0;
Nerd[] allNerds = new Nerd[]
{
    new Nerd("HaAyala", 100),
    new Nerd("HaAars", 0),
};

app.MapGet("/api/nerds", GetNerds);
app.MapGet("/api/score", GetLastScore);
app.MapPost("/api/add_nerd", AddNerd);
app.MapPost("/api/score", PostScore);

app.Run();


IResult GetNerds()
{
	string output = "";

	foreach (Nerd nerd in allNerds)
	{
		output += $"{nerd.name} is a {nerd.score}% nerd\n";
	}

	return Results.Text(output);
}

IResult GetLastScore()
{
	return Results.Text(userScore.ToString());
}

int GetScore(HttpRequest request)
{
	int score = 40;

	score += GetQuestion1Score(request.Form["question1"].ToString());
	score += GetQuestion2Score(request.Form["question2"].ToString());
	score += GetQuestion3Score(request.Form["question3"].ToString());
	score += GetQuestion4Score(request.Form["question4"].ToString());
	score += GetQuestion5Score(request.Form["question5"].ToString());
	score += GetQuestion6Score(request.Form["question6"].ToString());

	return score;
}
IResult PostScore(HttpRequest request)
{
	int score = GetScore(request);
	return Results.Text(score.ToString());
}


IResult AddNerd(HttpRequest request)
{
	string name = request.Form["name"].ToString();
	int score = GetScore(request);


	Nerd newNerd = new Nerd(name, score);
	Nerd[] tmpNerds = new Nerd[allNerds.Length + 1];
	int index = 0;
	bool added = false;

	foreach (Nerd nerd in allNerds)
	{
		if (!added && nerd.score < newNerd.score)
		{
			tmpNerds[index++] = newNerd;
			added = true;
		}

		tmpNerds[index++] = nerd;
	}

	if (!added)
	{
		tmpNerds[index] = newNerd;
	}

	allNerds = tmpNerds;
	userScore = score;
	return Results.Redirect("/nerdboard.html");
}

int GetQuestion1Score(string answer)
{
	if (answer == "1")
	{
		return 5;
	}
	else if (answer == "2")
	{
		return -20;
	}
	else if (answer == "3")
	{
		return 7;
	}

	return 10;
}

int GetQuestion2Score(string answer)
{
	if (answer == "1")
	{
		return 8;
	}
	else if (answer == "2")
	{
		return 8;
	}
	else if (answer == "3")
	{
		return 5;
	}

	return 8;
}

int GetQuestion3Score(string answer)
{
	if (answer == "1")
	{
		return -7;
	}
	else if (answer == "2")
	{
		return 4;
	}
	else if (answer == "3")
	{
		return 3;
	}

	return 10;
}
int GetQuestion4Score(string answer)
{
	if (answer == "1")
	{
		return 0;
	}
	else if (answer == "2")
	{
		return 4;
	}
	else if (answer == "3")
	{
		return 8;
	}

	return 10;
}
int GetQuestion5Score(string answer)
{
	if (answer == "1")
	{
		return -5;
	}
	else if (answer == "2")
	{
		return 10;
	}
	else if (answer == "3")
	{
		return 5;
	}

	return 10;
}
int GetQuestion6Score(string answer)
{
	if (answer == "1")
	{
		return 10;
	}
	else if (answer == "2")
	{
		return 10;
	}
	else if (answer == "3")
	{
		return 0;
	}

	return 9;
}



class Nerd
{
	public string name;
	public int score;

	public Nerd(string name, int score)
	{
		this.name = name;
		this.score = score;
	}
}
