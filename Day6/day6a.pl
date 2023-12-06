:- set_prolog_flag(double_quotes, chars).
:- use_module(library(dcg/basics)).
:- use_module(library(clpfd)).
:- use_module(library(yall)).
:- use_module(library(apply)).

% Time:      7  15   30
% Distance:  9  40  200
nums([]) --> [].
nums([T|Ts]) --> blanks, integer(T), blanks, nums(Ts).
races([Times,Distances]) --> blanks, "Time:", nums(Times), blanks, "Distance:", blanks, nums(Distances), blanks.

main :-
  read_file_to_string('input.txt', FStr, []),
  string_chars(FStr, FChars),
  phrase(races([Times, Distances]), FChars),
  maplist(sols, Times, Distances, NumsOfSolutions),
  foldl([X,Y,Z]>>(Z is X*Y), NumsOfSolutions, 1, P),
  writeln(P).

% succeeds when there are N possible integer times to reach the distance under time T.
sols(T, D, N) :-
  W #> 0, W #< T,
  Da #= (T-W)*W,
  Da #> D,
  fd_size(W, N).
  





