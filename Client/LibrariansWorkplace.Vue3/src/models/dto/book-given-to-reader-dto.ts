import { ReaderBookDto } from "./reader-book-dto";

export interface BookGivenToReaderDto {
  book: ReaderBookDto;
  count: number;
  dateOfIssue: Date;
}
