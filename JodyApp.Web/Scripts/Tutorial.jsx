class CommentBox extends React.Component {
    render() {
        return (
            <div className="commentBox" > This is a test of a CommentBox
            </div>

        );
    }
}

ReactDOM.render(<CommentBox />, document.getElementById('content'));