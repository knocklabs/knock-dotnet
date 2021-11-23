# Knock .NET Library

Knock API access for applications using .NET. Supports .NET Standard 2.0+ and .NET Framework 4.6.1+.

## Documentation

See the [API documentation](https://docs.knock.app) for usage examples.

## Installation

There are several options to install the Knock .NET SDK.

### Via the NuGet Package Manager

```
nuget install Knock.net
```

### Via the .NET Core Command Line Tools

```
dotnet add package Knock.net
```

### Via Visual Studio IDE

```
Install-Package Knock.net
```

## Configuration

To use the Knock client, you must provide an API key from the Knock dashboard.

```c#
using Knock;

var knockClient = new KnockClient(
  new KnockOptions { ApiKey = "sk_12345" });
```

## Usage

### Identifying users

```c#
var knockClient = new KnockClient(
  new KnockOptions { ApiKey = "sk_12345" });

var identifyUserParams = new Dictionary<string, object>{
	{ "name", "John Hammond" },
	{ "email", "jhammond@ingen.net" }
};

var user = await knockClient.Users.Identify("jhammond", identifyUserParams)
```

### Retrieving users

```c#
var knockClient = new KnockClient(
  new KnockOptions { ApiKey = "sk_12345" });

var user = await knockClient.Users.Get("jhammond")
```

### Triggering workflows

```c#
var knockClient = new KnockClient(
  new KnockOptions { ApiKey = "sk_12345" });

var workflowTrigger = TriggerWorkflow{
  // list of user ids for who should receive the notif
  Recipients = ["jhammond", "agrant", "imalcolm", "esattler"],
  // user id of who performed the action
  Actor: "dnedry",
  // an optional cancellation key
  CancellationKey: alert.Id,
  // an optional tenant
  Tenant: "jurassic-park",
  // data payload to send through
  Data: new Dictionary<string, object>{
    {"type", "trex"},
    {"priority", "1"}
  },
};

var result = await knockClient.Workflows.Trigger("dinosaurs-loose", workflowTrigger)
```

### Preferences

```c#
var knockClient = new KnockClient(
  new KnockOptions { ApiKey = "sk_12345" });

// Set preference set for user
var preferenceSetUpdate = SetPreferenceSet{
  ChannelTypes = new Dictionary<string, boolean> {
    {"email", false}
  }
};

var result = await knockClient.Preferences.Set("jhammond", preferenceSetUpdate);

// Set granular channel type preferences
var result = await knockClient.Preferences.SetChannelType("jhammond", "email", true);

// Set granular workflow preferences
var result = await knockClient.Preferences.SetWorkflow("jhammond", "dinosaurs-loose", false);

// Retrieve preferences
var preferenceSet = await knockClient.Preferences.get("jhammond");
```

### Getting and setting channel data

```c#
var knockClient = new KnockClient(
  new KnockOptions { ApiKey = "sk_12345" });

// Set channel data for an APNS channel
var channelData = new Dictionary<string, List<string>>{
  {"tokens", [apnsToken]},
};

var result = await knockClient.Users.SetChannelData("jhammond", knockApnsChannelId, channelData);

// Get channel data for the APNS channel
var result = await knockClient.Users.GetChannelData("jhammond", knockApnsChannelId);
```

### Canceling notifies

```c#
var knockClient = new KnockClient(
  new KnockOptions { ApiKey = "sk_12345" });

var cancelParams = WorkflowCancel{
  recipients: [],
}

knock.Workflows.cancel("dinosaurs-loose", alert.id, cancelParams)
```
