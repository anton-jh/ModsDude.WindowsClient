// Figure out how to make a configurator that can build this at runtime...
// OR make the configurator create JSON that then gets deserialized to this...?
//
// Figure out how to serialize function parameters. Non-pipeline ones might be
// serializable as-is, pipelines will need to be converted to PipelineItems.



//using ModsDude.WindowsClient.Experiments.Adapters;
using MoonSharp.Interpreter;

//await AdaptersTest.Run();


double MoonSharpFactorial()
{
    string scriptCode = @"    
		-- defines a factorial function
		function fact (n)
			if (n == 0) then
				return 1
			else
				return n*fact(n - 1)
			end
		end";

    var script = new Script();
    script.DoString(scriptCode);
    var res = script.Call(script.Globals["fact"], 4);
    return res.Number;
}


Console.WriteLine(MoonSharpFactorial());
