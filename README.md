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

var workflowTrigger = new TriggerWorkflow {
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
var preferenceSetUpdate = new SetPreferenceSet {
  ChannelTypes = new Dictionary<string, boolean> {
    {"email", false}
  }
};

var result = await knockClient.Users.SetPreferences("jhammond", preferenceSetUpdate);

// Update a single workflow preference, merging it into the existing preference set
var workflowUpdate = new SetPreferenceSet {
  Workflows = new Dictionary<string, object> {
    {"dinosaurs-loose", false}
  },
  PersistenceStrategy = "merge"
};

var result = await knockClient.Users.SetPreferences("jhammond", workflowUpdate);

// Retrieve preferences, optionally for a tenant
var preferenceSet = await knockClient.Users.GetPreferences("jhammond");
var tenantPreferenceSet = await knockClient.Users.GetPreferences(
  "jhammond",
  "default",
  new Dictionary<string, object> { {"tenant", "jurassic-park"} });

// Remove a preference set
await knockClient.Users.UnsetPreferences("jhammond");
```

The granular `SetChannelTypePreferences` and `SetWorkflowPreferences` methods are deprecated in favor of
`SetPreferences` with `PersistenceStrategy = "merge"`.

### Hosted preference center

```c#
var config = await knockClient.Users.GetPreferenceCenterConfig("jhammond");

var signedUrl = await knockClient.Users.GeneratePreferenceCenterSignedUrl("jhammond");
// signedUrl.Url, signedUrl.Token
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

var cancelParams = new CancelWorkflow {
  // Optional list of recipients to cancel the workflow run for
  Recipients = ["dnedry"],
  // The cancellation key that corresponds with the original workflow run
  CancellationKey = alert.id,
};

var result = await knockClient.Workflows.cancel("dinosaurs-loose", cancelParams);
```

### Updating message status

```c#
var knockClient = new KnockClient(
  new KnockOptions { ApiKey = "sk_12345" });

// Update a single message
var message = await knockClient.Messages.MarkAsRead(messageId);
var message = await knockClient.Messages.MarkAsInteracted(
  messageId,
  new Dictionary<string, object> { {"action", "clicked"} });
var message = await knockClient.Messages.Archive(messageId);

// Update a batch of messages
var messages = await knockClient.Messages.BatchMarkAsSeen(new List<string> { messageId, otherMessageId });

// Update every message sent through a channel that matches a set of filters
var bulkOperation = await knockClient.Channels.BulkUpdateMessageStatus(
  knockInAppChannelId,
  "archive",
  new BulkUpdateChannelMessagesOptions {
    RecipientIds = new List<string> { "jhammond" },
    OlderThan = "2024-01-01T00:00:00Z"
  });

// Inspect how a message was delivered
var deliveryLogs = await knockClient.Messages.GetDeliveryLogs(messageId);
```

### Reading a user's in-app feed

```c#
var feed = await knockClient.Users.GetFeedItems(
  "jhammond",
  knockInAppChannelId,
  new Dictionary<string, object> {
    {"status", "unread"},
    {"trigger_data", new Dictionary<string, object> { {"type", "trex"} }}
  });

// feed.entries, feed.Meta
```

### Tenants

```c#
// Set a tenant, resolving its full preference settings in the response
var tenant = await knockClient.Tenants.Set(
  "jurassic-park",
  new Dictionary<string, object> { {"name", "Jurassic Park"} },
  new Dictionary<string, object> { {"resolve_full_preference_settings", true} });

// Bulk set and delete tenants
var bulkOperation = await knockClient.Tenants.BulkSet(new BulkSetTenantsOptions {
  Tenants = new List<object> {
    "isla-sorna",
    new Dictionary<string, object> { {"id", "isla-nublar"}, {"name", "Isla Nublar"} }
  }
});

var bulkOperation = await knockClient.Tenants.BulkDelete(new List<string> { "isla-sorna", "isla-nublar" });
```

### Object subscriptions

```c#
var bulkOperation = await knockClient.Objects.BulkAddSubscriptions("projects", new BulkAddSubscriptionsOptions {
  Subscriptions = new List<BulkAddSubscriptionsOption> {
    new BulkAddSubscriptionsOption {
      Id = "visitor-center",
      Recipients = new List<object> { "jhammond", "agrant" }
    }
  }
});

// List the objects a user is subscribed to
var subscriptions = await knockClient.Users.ListSubscriptions("jhammond");
```

### Schedules

```c#
var schedules = await knockClient.Schedules.Create(new CreateSchedules {
  Workflow = "daily-park-report",
  Recipients = new List<object> { "jhammond" },
  Repeats = new List<Dictionary<string, object>> {
    new Dictionary<string, object> { {"frequency", "daily"}, {"hours", 9}, {"minutes", 0} }
  }
});

var page = await knockClient.Schedules.List("daily-park-report");

await knockClient.Schedules.Delete(new List<string> { schedules[0].Id });

// Create many schedules, each with a single recipient
var bulkOperation = await knockClient.Schedules.BulkCreate(new BulkCreateSchedulesOptions {
  Schedules = new List<BulkCreateScheduleOption> {
    new BulkCreateScheduleOption { Workflow = "daily-park-report", Recipient = "agrant" }
  }
});
```

### Audiences

```c#
var members = new AddAudienceMembersOptions {
  Members = new List<AudienceMemberOption> {
    new AudienceMemberOption {
      User = new Dictionary<string, object> { {"id", "jhammond"} },
      Tenant = "jurassic-park"
    }
  }
};

// Pass createAudience: true to create the audience if it does not exist yet
await knockClient.Audiences.AddMembers("park-staff", members, createAudience: true);

var page = await knockClient.Audiences.ListMembers("park-staff");
```

### Workflow recipient runs

```c#
var runs = await knockClient.WorkflowRecipientRuns.List(
  new Dictionary<string, object> { {"workflow", "dinosaurs-loose"} });

var run = await knockClient.WorkflowRecipientRuns.Get(runId);
// run.Status, run.Events
```
