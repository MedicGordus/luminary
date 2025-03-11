namespace luminary.util;

public static class ListExtentions
{
    public static bool TryGet<T>(this List<T> _list, T _search, out T? _result)
    {
        _result = _list.FirstOrDefault(_s => _s == null ? _search == null : _s.Equals(_search));
        return _result != null;
    }
}