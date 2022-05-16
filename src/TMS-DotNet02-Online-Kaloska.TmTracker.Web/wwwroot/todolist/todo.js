////$('#formtodo').submit(function () {
////    $.ajax({
////        url: "/Goal/AddGoal",
////        type: "POST",
////        data: $('#formtodo').serialize(),
////        success: function (result) {
////            if (result) {
////                //setTimeout(1000);
////                //location.reload();
////            }
////        }
////    })
////});
function post() {
    $.ajax({
        url: "/Goal/AddGoal",
        type: "POST",
        data: $('#formtodo').serialize(),
        success: function (result) {
            if (result) {
                setTimeout(1000);
                location.reload();
            }
        }
    });
    return false;
}

const todoInput = document.querySelector('.todo-input');
const todoList = document.querySelector('.todo-list');

//Event Listeners
todoList.addEventListener('click', deleteCheck);

function deleteCheck(e) {
    //console.log(e.target);
    const item = e.target;
    if (item.classList[0] === "trash-btn") {

        const todo = item.parentElement;
        todo.classList.add('fall');
        removeLocalTodos(todo);
        todo.addEventListener('transitionend', function () {
            todo.remove();
        });
        $.get("/Goal/DeleteGoal", { goalId: item.id });
    }
    if (item.classList[0] === "complete-btn") {
        const todo = item.parentElement;
        if (!todo.classList.contains("completed")) {
            $.get("/Goal/GoalAsCompleted", { goalId: item.id });
            todo.classList.add("completed");
        }
    }
};
function removeLocalTodos(todo) {
    let todos;
    if (localStorage.getItem('todos') === null) {
        todos = [];
    } else {
        todos = JSON.parse(localStorage.getItem('todos'));
    }
    const todoIndex = todo.children[0].innerText;
    todos.splice(todos.indexOf(todoIndex), 1);
    localStorage.setItem('todos', JSON.stringify(todos));
};