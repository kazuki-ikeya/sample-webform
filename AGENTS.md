# AGENTS.md

Guidance for agents working in this repository.

## Project Overview

- This is an ASP.NET Web Forms project targeting .NET Framework 4.8.
- Solution: `WebForm.sln`
- Project file: `WebForm.csproj`
- Pages use `.aspx`, code-behind uses `.aspx.cs`, and generated fields use `.aspx.designer.cs`.
- User controls live under `Controls/UserControls` and use `.ascx`, `.ascx.cs`, and `.ascx.designer.cs`.
- Shared layout is implemented in `Site.master` and `Site.master.cs`.
- Entity Framework 6.4.4 is referenced.

## General Rules

- Do not convert this project to ASP.NET Core, MVC, Razor Pages, or Blazor.
- Preserve the existing Web Forms structure and naming style.
- Keep changes scoped to the requested behavior.
- Do not rewrite unrelated mojibake text or comments. Some existing files contain garbled Japanese text.
- For new UI text and comments, prefer Japanese when the text is user-facing.
- Avoid adding dependencies unless the requested feature needs them.
- If external CDN assets are used, keep their usage scoped to the relevant user control or page.

## Web Forms Rules

- Put UI markup in `.aspx` and `.ascx`.
- Put event handlers, server-side properties, and behavior in `.aspx.cs` and `.ascx.cs`.
- Treat `.designer.cs` files as generated files. Edit them only when necessary to keep Web Forms field declarations in sync.
- Server-side controls that are accessed from code-behind must have `runat="server"` and an `ID`.
- When adding a new `.aspx`, `.ascx`, `.cs`, or `.designer.cs`, register it in `WebForm.csproj` as `Content` or `Compile`.
- Initial page/control setup should normally happen inside `if (!IsPostBack)`.
- Do not overwrite posted input values during postback unless that is explicitly intended.

## User Controls

- Place reusable UI parts in `Controls/UserControls`.
- A typical user control should include:
  - `Name.ascx`
  - `Name.ascx.cs`
  - `Name.ascx.designer.cs`
- Existing ReadOnly behavior generally hides the input control and shows a `Label` for display mode.
- If several controls share behavior, use a small base class or interface that fits the existing Web Forms style.

## Date Control Verification

The date-control comparison page is `DateControlVerification.aspx`.

Current comparison controls:

- `CommonDate`: browser-standard date input.
- `CommonCalendarDate`: ASP.NET `asp:Calendar` based date input.
- `CommonDatepickerDate`: jQuery UI datepicker based date input.
- `CommonFlatpickrDate`: flatpickr based date input.

Common date-control expectations:

- Display dates as `yyyy/MM/dd` where possible.
- In ReadOnly mode, show a label instead of an editable input.
- Add a calendar icon to the input.
- Color Saturdays blue.
- Color Sundays and Japanese holidays red.
- Shared date behavior, CSS, and JavaScript are in `CommonDateControlBase.cs`.

Important notes:

- Browser-native `input type="date"` does not allow styling individual calendar cells.
- `asp:Calendar` is a server control. Toggling, month navigation, and date selection can cause postback.
- `CommonCalendarDate` is intentionally implemented as a separate research/prototype control.
- jQuery UI datepicker and flatpickr initialization currently lives in each `.ascx` file, not in `.designer.cs`.
- Do not move runtime initialization logic into `.designer.cs`.

## Master Page And Menu

- Shared layout is in `Site.master`.
- The left menu is an `asp:TreeView` in `Site.master`.
- Add `NavigateUrl` to a `TreeNode` when a new page should be reachable from the menu.
- Add menu description text in `Site.master.cs` inside `GetDescription`.

## Build And Verification

- Prefer building `WebForm.sln` in Visual Studio or with MSBuild.
- `msbuild` may not be available on PATH in this environment. If so, report that the build could not be run.
- After changes, use `rg` to check:
  - Control IDs match designer field declarations.
  - New files are registered in `WebForm.csproj`.
  - Old property names or obsolete branches are not left behind.

## Encoding

- Prefer ASCII for agent-facing documentation such as this file.
- New user-facing Japanese text is acceptable in `.aspx` or `.ascx` when needed.
- Do not mass-convert file encodings unless explicitly requested.
