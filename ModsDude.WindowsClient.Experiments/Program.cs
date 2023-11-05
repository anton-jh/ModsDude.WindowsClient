// Figure out how to make a configurator that can build this at runtime...
// OR make the configurator create JSON that then gets deserialized to this...?
//
// Figure out how to serialize function parameters. Non-pipeline ones might be
// serializable as-is, pipelines will need to be converted to PipelineItems.



using ModsDude.WindowsClient.Experiments.Adapters;

await AdaptersTest.Run();
