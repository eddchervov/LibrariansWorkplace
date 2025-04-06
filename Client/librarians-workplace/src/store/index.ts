import { GetIssuedBooksDto } from "@/models/dto/get-issued-books-dto"
import { GetAvailableBooksDto } from "@/models/dto/get-available-books-dto"
import { GetBookDto } from "@/models/dto/books/get-book-dto"
import { CreateBookDto } from "@/models/dto/books/create-book-dto"
import { UpdateBookDto } from "@/models/dto/books/update-book-dto"
import { createStore } from "vuex"
import { CreateReaderDto } from "@/models/dto/readers/create-reader-dto"
import { Option } from "@/models/view-models/option"
import { GiveBookToReaderDto } from "@/models/dto/give-book-to-reader-dto"
import { ReturnBookToLibraryDto } from "@/models/dto/return-book-to-library-dto"
import { UpdateReaderVm } from "@/models/view-models/update-reader-vm"
import axios from "axios"
import serverPath from "./server-path"
import { UpdateBookVm } from "@/models/view-models/update-book-vm"
import { ReaderOption } from "@/models/view-models/reader-option"

export default createStore({
  state: {
    issuedBooks: new Array<GetIssuedBooksDto>(),
    availableBooks: new Array<GetAvailableBooksDto>(),
    foundBooks: new Array<GetBookDto>(),
    booksTab: 'AvailableBooks',
    // можно объединить 3 поля выбора книг, вообще в 1 на будущее
    // регулировать обнуление по компонентное, и если нужно будет доп поле, использовать local
    selectedGiveBookToReaderBook: null,
    selectedBookFromTabTakeBookFromReader: null,

    // books module start
    bookOptions: new Array<Option>(),
    selectedBookOption: null,
    selectedBook: null,
    bookManagementTab: "list",
    // books module end

    // reader module start
    readerOptions: new Array<ReaderOption>(),
    selectedReaderOption: null,
    selectedReader: null,
    readerManagementTab: "list",
    // reader module end
  },
  getters: {
    issuedBooks: (state) => state.issuedBooks as GetIssuedBooksDto[],
    availableBooks: (state) => state.availableBooks as GetAvailableBooksDto[],
  },
  mutations: {
    setSelectedGiveBookToReaderBook: (state, book) => {
      state.selectedGiveBookToReaderBook = book
    },
    setSelectedBookFromTabTakeBookFromReader: (state, book) => {
      state.selectedBookFromTabTakeBookFromReader = book
    },
    setBooksTab: (state, tab) => { 
      state.booksTab = tab
    },
    setIssuedBooks: (state, issuedBooks) => {
      state.issuedBooks = issuedBooks
    },

    setAvailableBooks: (state, availableBooks) => {
      state.availableBooks = availableBooks
    },
    setFoundBooks: (state, books) => {
      state.foundBooks = books
    },
    // books module start
    setSelectedBook: (state, book) => {
      state.selectedBook = book
    },
    setSelectedBookOption: (state, book) => {
      state.selectedBookOption = book
    },
    setBookOptions: (state, bookOptions) => {
      state.bookOptions = bookOptions
    },
    addBookOption: (state, book) => {
      state.bookOptions.push(book)
    },
    changeBookOption: (state, book) => {
      const bo = state.bookOptions.find((x) => x.id == book.id)
      if (bo) {
        bo.name = book.name
      }
    },
    deleteBookOption: (state, bookId) => {
      const bo = state.bookOptions.find((x) => x.id == bookId)
      if (bo) {
        const index = state.bookOptions.indexOf(bo)
        if (index !== -1) state.bookOptions.splice(index, 1)
      }
    },
    setBookManagementTab: (state, tab) => {
      state.bookManagementTab = tab
    },
    // books module end

    // reader module start
    setSelectedReader: (state, reader) => {
      state.selectedReader = reader
    },
    setSelectedReaderOption: (state, reader) => {
      state.selectedReaderOption = reader
    },
    setReaderOptions: (state, readers) => {
      state.readerOptions = readers
    },
    addReaderOption: (state, reader) => {
      state.readerOptions.push(reader)
    },
    changeReaderOption: (state, reader) => {
      const bo = state.readerOptions.find((x) => x.id == reader.id)
      if (bo) {
        bo.name = reader.name
        bo.dateBirth = reader.dateBirth
      }
    },
    deleteReaderOption: (state, readerId) => {
      const bo = state.readerOptions.find((x) => x.id == readerId)
      if (bo) {
        const index = state.readerOptions.indexOf(bo)
        if (index !== -1) state.readerOptions.splice(index, 1)
      }
    },
    setReaderManagementTab: (state, tab) => {
      state.readerManagementTab = tab
    },
    // reader module end
  },
  actions: {
    // module BookManagement start
    loadBookManagement({ commit }) {
      this.dispatch("getBookOptions")
    },
    async getBookById({ commit }, id: number) {
      await axios
        .get(serverPath + "/books/" + id)
        .then((p) => {
          commit("setSelectedBook", p.data)
        })
        .catch((e) => {
          alert("Произошла ошибка получения книги")
        })
    },
    async getBookOptions({ commit }) {
      await axios
        .get(serverPath + "/books/options")
        .then((p) => {
          commit("setBookOptions", p.data)
        })
        .catch((e) => {
          alert("Произошла ошибка получения списка книг")
        })
    },
    async createBook({ commit }, dto: CreateBookDto) {
      await axios
        .post(serverPath + "/books", dto)
        .then((p) => {
          commit("addBookOption", {
            id: p.data,
            name: `${dto.name}, ${dto.author} ${dto.yearPublication} г.`,
          })
        })
        .catch((e) => {
          alert("Произошла ошибка создания книги")
        })
    },
    async updateBook({ commit }, vm: UpdateBookVm) {
      await axios
        .put(serverPath + "/books/" + vm.id, vm.dto)
        .then((p) => {
          commit("changeBookOption", {
            id: vm.id,
            name: vm.dto.name,
          })
        })
        .catch((e) => {
          alert("Произошла ошибка обновления книги")
        })
    },
    async deleteBook({ commit }, bookdId: number) {
      await axios
        .delete(serverPath + "/books/" + bookdId)
        .then((p) => {
          commit("deleteBookOption", bookdId)
        })
        .catch((e) => {
          alert("Произошла ошибка удаления книги")
        })
    },
    // module BookManagement end

    // module ReaderManagement start
    loadReaderManagement({ commit }) {
      this.dispatch("getReaderOptions")
    },
    async getReaderById({ commit }, readerId: number) {
      await axios
        .get(serverPath + "/readers/" + readerId)
        .then((p) => {
          console.log(p.data)
          commit("setSelectedReader", p.data)
        })
        .catch((e) => {
          alert("Произошла ошибка получения читателя")
        })
    },
    async getReaderOptions({ commit }) {
      await axios
        .get(serverPath + "/readers/options")
        .then((p) => {
          commit("setReaderOptions", p.data)
        })
        .catch((e) => {
          alert("Произошла ошибка получения списка читателей")
        })
    },
    async createReader({ commit }, dto: CreateReaderDto) {
      await axios
        .post(serverPath + "/readers", dto)
        .then((p) => {
          commit("addReaderOption", {
            id: p.data,
            name: dto.fullName,
            dateBirth: dto.dateBirth
          })
        })
        .catch((e) => {
          alert("Произошла ошибка создания читателя")
        })
    },
    async updateReader({ commit }, vm: UpdateReaderVm) {
      await axios
        .put(serverPath + "/readers/" + vm.id, vm.dto)
        .then((p) => {
          commit("changeReaderOption", {
            id: vm.id,
            name: vm.dto.fullName,
            dateBirth: vm.dto.dateBirth
          })
        })
        .catch((e) => {
          alert("Произошла ошибка обновления читателя")
        })
    },
    async deleteReader({ commit }, readerId: number) {
      await axios
        .delete(serverPath + "/readers/" + readerId)
        .then((p) => {
          commit("deleteReaderOption", readerId)
        })
        .catch((e) => {
          alert("Произошла ошибка удаления читателя")
        })
    },
    // module ReaderManagement end

    async getIssuedBooks({ commit }) {
      await axios
        .get(serverPath + "/books/issued-books")
        .then((p) => {
          commit("setIssuedBooks", p.data)
        })
        .catch((e) => {
          alert("Произошла ошибка получения списка выданных книг")
        })
    },
    async getAvailableBooks({ commit }) {
      await axios
        .get(serverPath + "/books/available-books")
        .then((p) => {
          commit("setAvailableBooks", p.data)
        })
        .catch((e) => {
          alert("Произошла ошибка получения списка доступных для выдачи книг")
        })
    },
    async giveBookToReader({ commit }, dto: GiveBookToReaderDto) {
      await axios
        .post(serverPath + "/book-management/give-book-to-reader", dto)
        .then((p) => {
          // для упрощения, можно найти в списке книг и сделать свойству - 1 кол-ва доступных,
          // там по условию когда будет 0 то запрет со стороны фронта на выдачу 
          this.dispatch("getAvailableBooks")
          this.dispatch("getIssuedBooks")
        })
        .catch((e) => {
          alert("Произошла ошибка выдачи книги")
        })
    },
    async returnBookToLibrary({ commit }, dto: ReturnBookToLibraryDto) {
      await axios
        .post(serverPath + "/book-management/return-book-to-library", dto)
        .then((p) => {
          this.dispatch("getAvailableBooks")
          this.dispatch("getIssuedBooks")
        })
        .catch((e) => {
          alert("Произошла ошибка сдачи книги")
        })
    },

    async booksSearchByName({ commit }) {
      await axios
        .get(serverPath + "/books/search-by-name")
        .then((p) => {
          commit("setFoundBooks", p.data)
        })
        .catch((e) => {
          alert("Произошла ошибка поиска книг")
        })
    },

    async loadBooksPage({ commit }) {
      await this.dispatch("getIssuedBooks")
      await this.dispatch("getAvailableBooks")
      await this.dispatch("getReaderOptions")
    },
  },
  modules: {},
})
