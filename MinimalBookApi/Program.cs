var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
                                         
var books = new List<Book> {
    new Book { Id = 1, Title = "O Pequeno Príncipe", Author = " Antoine de Saint-Exupéry."},
    new Book { Id = 2, Title = "Romeu E Julieta", Author = "William Shakespeare"},
    new Book { Id = 3, Title = "O universo numa casca de noz", Author = "Stephen Hawking"},
    new Book { Id = 4, Title = "Física Quântica: Átomos, Moléculas, Sólidos e Partículas", Author = "Robert Eisberg"},
    new Book { Id = 5, Title = "Teoria da Relatividade", Author = "Albert Einstein"},
    new Book { Id = 6, Title = "Breves Respostas para Grandes Questões", Author = "Stephen Hawking"}
};


app.MapGet("/book", () =>
{
    return books;
});

app.MapGet("/book/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("Desculpe, Esse Livro Não existe");

    return Results.Ok(book);
});

app.MapPost("/books", (Book book) =>
{
    books.Add(book);
    return books;
});

app.MapPut("/book/{id}", (Book updateBook, int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("Desculpe, Esse Livro Não existe");
    book.Title = updateBook.Title;
    book.Author = updateBook.Author;

    return Results.Ok(book);
});

app.MapDelete("/book/{id}", ( int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("Desculpe, Esse Livro Não existe");
   
    books.Remove(book); 
    return Results.Ok(book);
});

app.Run();
                     
class Book
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Author { get; set; }
}




