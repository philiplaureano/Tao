namespace CustomAttributes
{
    [Sample(42)] // boxed 42
    // 01 00 // Prolog
    // 08 // I4
    // 2a 00 00 00 // 0x0000002A
    // 00 00 // NumNamed
    //[Sample(obj = 7)] // named field
    // 01 00 // Prolog
    // 01 00 // NumNamed
    // 53 51 // FIELD, OBJECT

    // 03 6f 62 6a // "obj" as counted-UTF8
    // 08 // I4
    // 07 00 00 00 // 0x00000007
    //[Sample(SomeProperty = 0xEE)] // named property
    // 01 00 // Prolog
    // 01 00 // NumNamed
    // 54 51 // PROPERTY, OBJECT
    // 01 6f // "o" as counted-UTF8
    // 08 // I4
    // ee 00 00 00 // 0x000000EE
    class App { static void Main() { } }
}
