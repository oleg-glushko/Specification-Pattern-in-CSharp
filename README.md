[Specification Pattern in C#](http://www.pluralsight.com/courses/csharp-specification-pattern) Pluralsight course's rewrite in .NET 8.

Create a database using the "Database.sql" or "DatabaseWithDirector.sql" (only for the lab 04-2) files and adjust a connection string in "App.xaml.cs" of an appropritate project.

**Be aware** this pattern isn't a good choice for most real-life projects. Let me quote Vladimir himself ([full article](https://enterprisecraftsmanship.com/posts/cqrs-vs-specification-pattern/)):

> Unfortunately, the Specification pattern works only in simple cases, where you don’t need sophisticated querying logic. And in such cases, it may be fine to use this pattern.
>
> However, in large systems, you should almost always choose the loose coupling over preventing the domain knowledge duplication between the reads and writes. The freedom to choose the most appropriate solution for the problem at hand trumps the DRY principle.
