import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import { GetIssuedBooksDto } from "@/models/dto/get-issued-books-dto";
import { GetAvailableBooksDto } from "@/models/dto/get-available-books-dto";
import { GetBookDto } from "@/models/dto/get-book-dto";
import { CreateBookDto } from "@/models/dto/create-book-dto";
import { UpdateBookDto } from "@/models/dto/update-book-dto";
import { CreateReaderDto } from "@/models/dto/create-reader-dto";
import { Option } from "@/models/view-models/option";
import { GiveBookToReaderDto } from "@/models/dto/give-book-to-reader-dto";
import { ReturnBookToLibraryDto } from "@/models/dto/return-book-to-library-dto";
import { UpdateReaderVm } from "@/models/view-models/update-reader-vm";

Vue.use(Vuex);

const serverPath = "https://localhost:44397";

export default new Vuex.Store({
  state: {
    issuedBooks: new Array<GetIssuedBooksDto>(),
    availableBooks: new Array<GetAvailableBooksDto>(),
    foundBooks: new Array<GetBookDto>(),
    readerOptions: new Array<Option>(),
    selectedBook: null,
    selectedReader: null,
  },
  getters: {
    issuedBooks: (state) => state.issuedBooks,
    availableBooks: (state) => state.availableBooks,
    selectedBook: (state) => state.selectedBook
  },
  mutations: {
    setIssuedBooks: (state, issuedBooks) => {
      state.issuedBooks = issuedBooks;
    },

    setAvailableBooks: (state, availableBooks) => {
      state.availableBooks = availableBooks;
    },
    setFoundBooks: (state, books) => {
      state.foundBooks = books;
    },
    setReaderOptions: (state, readers) => {
      state.readerOptions = readers;
    },
    addReaderOption: (state, reader) => {
      state.readerOptions.push(reader);
    },
    setSelectedReader: (state, reader) => {
      state.selectedReader = reader;
    },
    setSelectedBook: (state, book) => {
      state.selectedBook = book
    },
    updateReader: (state, vm) => {
      const reader = state.readerOptions.find((x) => x.id == vm.id);
      if (!reader) return new Error("не найден читатель с id=" + vm.id);
      reader.name = vm.fullName;
    },
    deleteReader: (state, readerId) => {
      const reader = state.readerOptions.find((x) => x.id == readerId);
      if (!reader) return new Error("не найден читатель с id=" + readerId);
      const index = state.readerOptions.indexOf(reader)
      if (index !== -1) {
        state.readerOptions.splice(index, 1)
      }
    }
  },
  actions: {
    loadBooksPage({ commit }) {
      this.dispatch("getIssuedBooks") 
      this.dispatch("getAvailableBooks")
    },
    async giveBookToReader({ commit }, dto: GiveBookToReaderDto) {
      axios
        .post(serverPath + "/book-management/give-book-to-reader", dto)
        .then((p) => {
          this.dispatch("getIssuedBooks");
          this.dispatch("getAvailableBooks");
        })
        .catch((e) => {
          alert("Произошла ошибка выдачи книги");
        });
    },
    async returnBookToLibrary({ commit }, dto: ReturnBookToLibraryDto) {
      axios
        .post(serverPath + "/book-management/return-book-to-library", dto)
        .then((p) => {
          this.dispatch("getIssuedBooks");
          this.dispatch("getAvailableBooks");
        })
        .catch((e) => {
          alert("Произошла ошибка сдачи книги");
        });
    },
    async getReaderById({ commit }, readerId: number) {
      axios
        .get(serverPath + "/readers/" + readerId)
        .then((p) => {
          commit("setSelectedReader", p.data);
        })
        .catch((e) => {
          alert("Произошла ошибка получения читателя");
        });
    },
    async getReaderOptions({ commit }) {
      axios
        .get(serverPath + "/readers/options")
        .then((p) => {
          commit("setReaderOptions", p.data);
        })
        .catch((e) => {
          alert("Произошла ошибка получения списка читателей");
        });
    },
    async createReader({ commit }, dto: CreateReaderDto) {
      axios
        .post(serverPath + "/readers", dto)
        .then((p) => {
          commit("addReaderOption", {
            id: p.data.id,
            name: dto.fullName,
          });
        })
        .catch((e) => {
          alert("Произошла ошибка создания читателя");
        });
    },
    async updateReader({ commit }, vm: UpdateReaderVm) {
      axios
        .put(serverPath + "/readers/" + vm.id, vm.dto)
        .then((p) => {
          commit("updateReader", vm);
        })
        .catch((e) => {
          alert("Произошла ошибка обновления читателя");
        });
    },
    async deleteReader({ commit }, readerId: number) {
      axios
        .delete(serverPath + "/readers/" + readerId)
        .then((p) => {
          commit("deleteReader", readerId);
        })
        .catch((e) => {
          alert("Произошла ошибка удаления читателя");
        });
    },

    async createBook({ commit }, dto: CreateBookDto) {
      axios
        .post(serverPath + "/books", dto)
        .then((p) => {
          // по ui не понятно, какие списки и для чего
          this.dispatch("getIssuedBooks");
          this.dispatch("getAvailableBooks");
        })
        .catch((e) => {
          alert("Произошла ошибка создания книги");
        });
    },
    async updateBook({ commit }, dto: UpdateBookDto) {
      axios
        .put(serverPath + "/books", dto)
        .then((p) => {
          // по ui не понятно, какие списки и для чего
          this.dispatch("getIssuedBooks");
          this.dispatch("getAvailableBooks");
        })
        .catch((e) => {
          alert("Произошла ошибка обновления книги");
        });
    },
    async deleteBook({ commit }, bookdId: number) {
      axios
        .delete(serverPath + "/books/" + bookdId)
        .then((p) => {
          // по ui не понятно, какие списки и для чего
          this.dispatch("getIssuedBooks");
          this.dispatch("getAvailableBooks");
        })
        .catch((e) => {
          alert("Произошла ошибка удаления книги");
        });
    },
    async booksSearchByName({ commit }) {
      axios
        .get(serverPath + "/books/search-by-name")
        .then((p) => {
          commit("setFoundBooks", p.data);
        })
        .catch((e) => {
          alert("Произошла ошибка поиска книг");
        });
    },
    async getIssuedBooks({ commit }) {
      axios
        .get(serverPath + "/books/issued-books")
        .then((p) => {
          commit("setIssuedBooks", p.data);
        })
        .catch((e) => {
          alert("Произошла ошибка получения списка выданных книг");
        });
    },
    async getAvailableBooks({ commit }) {
      axios
        .get(serverPath + "/books/available-books")
        .then((p) => {
          commit("setAvailableBooks", p.data);
        })
        .catch((e) => {
          alert("Произошла ошибка получения списка доступных для выдачи книг");
        });
    },
  },
});
