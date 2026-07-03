WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

string greetingMessage = "Welcome to the Internet Project Guide";
string allMessages = "Hello from the server";
Nerd[] allNerds = new Nerd[]
{
    new Nerd("HaAyala", 100),
    new Nerd("HaAars", 0),
};

app.MapGet("/api/greeting", GetGreeting);
app.MapGet("/api/message", GetMessage);
app.MapGet("/api/nerds", GetNerds);
app.MapGet("/api/current-time", GetCurrentTime);
app.MapPost("/api/add_nerd", AddNerd);
app.MapPost("/api/message", UpdateMessage);

app.Run();

IResult GetGreeting()
{
	return Results.Text(greetingMessage);
}

IResult GetMessage()
{
	return Results.Text(allMessages);
}

IResult GetNerds()
{
	string output = "";

	foreach (Nerd nerd in allNerds)
	{
		output += $"{nerd.name} is a {nerd.score}% nerd\n";
	}

	return Results.Text(output);
}

IResult UpdateMessage(HttpRequest request)
{
	string message = request.Form["message"].ToString();
	if (!string.IsNullOrEmpty(message))
	{
		string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		allMessages += $"\n[{timestamp}] {message}";
	}
	return Results.Redirect("/examples.html");
}

IResult AddNerd(HttpRequest request)
{
	string name = request.Form["name"].ToString();
	int score = 40;

	score += GetQuestion1Score(request.Form["question1"].ToString());
	score += GetQuestion2Score(request.Form["question2"].ToString());
	score += GetQuestion3Score(request.Form["question3"].ToString());

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
		return 10;
	}
	else if (answer == "3")
	{
		return 15;
	}

	return 0;
}

int GetQuestion2Score(string answer)
{
	if (answer == "1")
	{
		return 5;
	}
	else if (answer == "2")
	{
		return 10;
	}
	else if (answer == "3")
	{
		return 15;
	}

	return 0;
}

int GetQuestion3Score(string answer)
{
	if (answer == "1")
	{
		return 5;
	}
	else if (answer == "2")
	{
		return 10;
	}
	else if (answer == "3")
	{
		return 15;
	}

	return 0;
}

IResult GetCurrentTime()
{
	var now = DateTime.Now;
	var formatted = now.ToString("dd/MM/yyyy HH:mm:ss");
	return Results.Text(formatted);
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
