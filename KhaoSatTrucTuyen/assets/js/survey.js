const question = `
    <div class="item-question">
        <div class="format-question">
            <div class="left-question">
                <input name="title-question" type="text" class="title-question" placeholder="Câu hỏi" autocomplete="off">
            </div>
            <div class="right-question">
                <select class="Kind-of-question">
                    <option value="doan">Đoạn</option>
                    <option value="tracnhiem">Trắc nhiệm</option>
                    <option value="hopkiem">Hộp kiểm</option>
                    <option value="menuthaxuong">Menu thả xuống</option>
                    <option value="">Tải tệp lên</option>
                </select>
            </div>
        </div>
        <div class="answer">
            <input type="hidden" class="sl_answer" value="1" />
            <input name="title-answer" type="text" class="title-answer" placeholder="Văn bản trả lời dài" autocomplete="off">
        </div>
        <div class="action-question">
            <i class="fas fa-trash-alt"></i>
        </div>
    </div>`;
const doan = `
    <input disabled name="title-answer" type="text" class="title-answer" placeholder="Văn bản trả lời dài" autocomplete="off">
`;
const tracnhiem = `
    <div class="item-radio-answer item-answer">
        <input type="radio" disabled>
        <input name="title-answer" type="text" class="title-answer" placeholder="Tùy chọn" autocomplete="off">
        <i class="fas fa-times fa-xs"></i>
    </div>
`;
const hopkiem = `
    <div class="item-checkbox-answer item-answer">
        <input type="checkbox" disabled>
        <input name="title-answer" type="text" class="title-answer" placeholder="Tùy chọn" autocomplete="off">
        <i class="fas fa-times fa-xs"></i>
    </div>
`;
const menuthaxuong = `
    <div class="item-select-answer item-answer">
        <i class="far fa-caret-square-down"></i>
        <input name="title-answer" type="text" class="title-answer" placeholder="Tùy chọn" autocomplete="off">
        <i class="fas fa-times fa-xs"></i>  
    </div>
`;

const add = `
    <div class="add-item-answer">
        <i class="fas fa-plus fa-xs"></i>
        <span>Thêm tùy chọn</span>
    </div>
`;
$(document).ready(function () {
    $(document).on("change", ".Kind-of-question", function () {
        let koq = $(this);
        let id = koq.closest(".item-question").attr("data-id");
        let answer = koq.closest(".item-question").find(".answer");
        let html_answer = `<input type="hidden" class="sl_answer" value="1" />`;
        obj_answer = $(`${html_answer}`).attr("name", "sl_answer" + id);
        switch (koq.val()) {
            case "doan":
                koq.closest(".item-question").find(".answer").html(doan);
                answer.append(obj_answer);
                break;
            case "tracnhiem":
                var obj_tracnhiem = $(`${tracnhiem}`);
                obj_tracnhiem.find(".title-answer").attr("name", "tracnhiem_" + id + "_1");
                answer.html(obj_tracnhiem);
                answer.append(obj_answer);
                answer.append(add);
                break;
            case "hopkiem":
                var obj_hopkiem = $(`${hopkiem}`);
                obj_hopkiem.find(".title-answer").attr("name", "hopkiem_" + id + "_1");
                answer.html(obj_hopkiem);
                answer.append(obj_answer);
                answer.append(add);
                break;
            case "menuthaxuong":
                var obj_menuthaxuong = $(`${menuthaxuong}`);
                obj_menuthaxuong.find(".title-answer").attr("name", "menuthaxuong_" + id + "_1");
                answer.html(obj_menuthaxuong);
                answer.append(obj_answer);
                answer.append(add);
                break;
            default: break;
        }
    })
    $(document).on("click", ".action-question i", function () {
        let item_question_remove = $(this).closest(".item-question");
        item_question_remove.remove();
    });
    $(document).on("click", ".item-answer .fa-times", function () {
        let item_answer_remove = $(this).closest(".item-answer");
        item_answer_remove.remove();
    });
    $("#add-question i").click(function () {
        let sl_question = $(".sl_question");
        let id_new = +sl_question.val() + +1;
        sl_question.val(id_new)
        obj_question = $(`${question}`);
        obj_question.attr("data-id", id_new);
        obj_question.find(".Kind-of-question").attr("name", "Kind_of_question_" + id_new)
        obj_question.find(".title-question").attr("name", "title_question_" + id_new);
        obj_question.find(".title-answer").attr("name", "title_answer_" + id_new);
        obj_question.find(".sl_answer").attr("name", "sl_answer" + id_new);
        $(this).closest(".container").find(".question-survey").append(obj_question);
    });
    $(document).on("click", ".add-item-answer", function () {
        let koq = $(this).closest(".item-question").find(".Kind-of-question").val();
        let sl_answer = $(this).closest(".item-question").find(".sl_answer");
        let sl_new = +sl_answer.val() + +1;
        sl_answer.val(sl_new);
        switch (koq) {
            case "tracnhiem":
                var obj_tracnhiem = $(`${tracnhiem}`);
                obj_tracnhiem.find(".title-answer").attr("name", "tracnhiem_" + $(this).closest(".item-question").attr("data-id") + "_" + sl_new);
                $(obj_tracnhiem).insertBefore($(this));
                break;
            case "hopkiem":
                var obj_hopkiem = $(`${hopkiem}`);
                obj_hopkiem.find(".title-answer").attr("name", "hopkiem_" + $(this).closest(".item-question").attr("data-id") + "_" + sl_new);
                $(obj_hopkiem).insertBefore($(this));
                break;
            case "menuthaxuong":
                var obj_menuthaxuong = $(`${menuthaxuong}`);
                obj_menuthaxuong.find(".title-answer").attr("name", "menuthaxuong_" + $(this).closest(".item-question").attr("data-id") + "_" + sl_new);
                $(obj_menuthaxuong).insertBefore($(this));
                break;
            default: break;
        }

    })
    $('#openmodale').click(function (e) {
        e.preventDefault();
        $('.modale').addClass('opened');
   });
    $('.closemodale').click(function (e) {
        e.preventDefault();
        $('.modale').removeClass('opened');
    });
})
