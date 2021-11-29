<p align='center'>
  <img src="https://imgur.com/rd2H1i5.png" />
</p>

![pkg version](https://img.shields.io/gh/v/sharkgamedev/Sharp)
![downloads](https://img.shields.io/gh/dm/sharkgamedev/Sharp)
![gh types](https://img.shields.io/gh/types/sharkgamedev/Sharp)

# Sharp Engine
**Sharp** is an [OpenGL](https://www.opengl.org/) C# game engine using [ECS](https://github.com/SanderMertens/ecs-faq) and [OpenTK](https://opentk.net/) bindings with a strong focus on doing 2D well and being easy to learn.

## Functionality
- Fully customisable rendering
- Performant **component** system allowing for extensible behaviours
- Performance from the get-go
- **Easy** to use dev-suite
- **Thorough documentation** and code comments
- ECS based engine

## Usage:
```
nuget install @sharkgamedev/sharp (NUGET pkg coming soon)
```

## Quick Examples:
#### Writing custom renderer
```cs
public class CustomSpriteRenderer : SpriteRenderer
{
	public override void OnRender()
	{
		// Please don't do any of this, this is purely for example
		GL.BindBuffer(BufferTarget.ArrayBuffer, GL.GenBuffer());
		GL.BufferData(...);
		GL.DrawElements(...);
		// etc..
	}
}

```

### Custom component
```cs
public class CustomComponent : IComponent
{
	public override void OnLoad()
	{
		// etc...
	}

	public override void OnRender()
	{
		// etc...
	}

	public override void Update()
	{
		// etc..
	}
}

```

## Features:
- **ECS** efficient ECS pattern
- **JSON** scene and project data stored in JSON for human readability
- **Scene Graph** stay organized
- **Editor** Fully featured editor
- **Code comments** and <params> for in-editor code suggestions
- **Editor Hot-Reload** quickly test changes to your project
- **Hackable** supports custom builds of the engine
- **Steamworks** support for popular steamworks C# bindings

## How it works:
This engine is a simplified interface to OpenTK providing the framework and means you need to build an awesome 2D game. Many engines try to do so many things nowadays
and fall short on 2D tooling/performance because of it. Sharp aims to fix that and use a beginner friendly, easy and efficient language: C#.

**Read the wiki for extra documentation.**
  
Project created and maintained by Sharks Interactive.
  
### Developing:
  - Commit to ``staging`` and pr to ``prod`` for changes

### Code Style:
  - Continious Integration will handle formatting for you
  - Use formatters locally using google-code style to catch errors pre-pr

## Acknowledgements:
OpenTK
