using JsonDiffPatchDotNet;
using JsonDiffPatchDotNet.Formatters.JsonPatch;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Operation = Microsoft.AspNetCore.JsonPatch.Operations.Operation;

namespace Nabs.Core.Patching;

public static class PatchingExtensions
{
	private static readonly JsonDiffPatch JsonDiffPatch = new();
	private static readonly JsonDeltaFormatter Formatter = new();

	public static T DeepClone<T>(this T objectToClone)
		where T : class, new()
	{
		return JToken.FromObject(objectToClone)!.ToObject<T>()!;
	}

	public static IList<Operation> CreatePatchOperations<T>(this T original, T modified)
		where T : class, new()
	{
		var originalJToken = JToken.FromObject(original);
		var modifiedJToken = JToken.FromObject(modified);
		var jPatch = JsonDiffPatch.Diff(originalJToken, modifiedJToken);
		var formatterOperations = Formatter.Format(jPatch);

		var operationsJson = JsonConvert.SerializeObject(formatterOperations, Formatting.Indented);
		var operations = JsonConvert.DeserializeObject<IList<Operation>>(operationsJson)!;
		return operations;
	}

	public static JsonPatchDocument<T> CreatePatchDocument<T>(this T original, T modified)
		where T : class, new()
	{
		var originalJToken = JToken.FromObject(original);
		var modifiedJToken = JToken.FromObject(modified);
		var jPatch = JsonDiffPatch.Diff(originalJToken, modifiedJToken);
		var formatterOperations = Formatter.Format(jPatch);

		var operationsJson = JsonConvert.SerializeObject(formatterOperations, Formatting.Indented);
		var operations = JsonConvert.DeserializeObject<List<Operation<T>>>(operationsJson)!;
		var result = new JsonPatchDocument<T>();
		result.Operations.AddRange(operations);
		return result;
	}

	public static T MergePatchDocument<T>(this JsonPatchDocument<T> patch, T objectToApplyTo)
		where T : class, new()
	{
		patch.ApplyTo(objectToApplyTo);
		return objectToApplyTo;
	}
}