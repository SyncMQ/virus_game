namespace DataStructures;

public class Stapel<T> {

    private int _length;
    private Node? _top = null;
    
    public void Duw(T inhoud) {
        _top = new Node(inhoud, _top);
        _length++;
    }
    
    public T Pak() {
        var tempTop = _top.PakInhoud();
        _top = _top.PakVolgendeNode();
        _length--;
        return tempTop;
    }

    public int PakLength() {
        return _length;
    }

    private class Node {
        
        private T _inhoud;
        private Node? _volgendeNode;
        
        public Node(T inhoud, Node? volgendeNode) {
            _inhoud = inhoud;
            _volgendeNode = volgendeNode;
        }

        public T PakInhoud() {
            return _inhoud;
        }

        public Node? PakVolgendeNode() {
            return _volgendeNode;
        }

    }
}
