:- module(helpers, [split_all/3, split_all/4, split_nth0/4, split_nth0/5,
  sublists/4, replaceif/3, countall/2, string_number/2, nnth0/4, firstn/3, firstn/4, lastn/3]).

:- use_module(library(dcg/basics)).
:- use_module(library(clpfd)).
:- use_module(library(yall)).
:- use_module(library(apply)).

split_all(Seperator, Str, Parts) :-
  split_all(Seperator, ' ', Str, Parts).
split_all(Seperator, Ignore, Str, Parts) :-
  split_string(Str, Seperator, Ignore, Parts). 

split_nth0(Seperator, Index, Str, Part) :-
split_nth0(Seperator, ' ', Index, Str, Part).
split_nth0(Seperator, Ignore, Index, Str, Part) :-
  split_all(Seperator, Ignore, Str, Parts),
  nth0(Index, Parts, Part).

% replaceif(+Dict, +List1, -List2) is det.
% Succeeds if List2 has elements of List1 replaced by the given Dict,
% (key from List1 gives value from List2). The elements that are not 
% matched by Dict have the same value on List1 and List2.
replaceif(Dict, List1, List2) :-
  maplist({Dict}/[E1,E2]>>(E2=Dict.get(E1)->true;E2=E1), List1, List2).

sublist([], _, 0).
sublist([X|T], [X|Rest], Len) :-
  Len2 #= Len-1,
  sublist(T, Rest, Len2).
sublists(Sub, List, 0, Len) :- sublist(Sub, List, Len).
sublists(Sub, [_|Rest], At, Len) :- At2 #= At - 1, sublists(Sub, Rest, At2, Len).
  
% countall(+Goal, ?N) is det.
% Succeeds when Goal succeeds exactly N times.
countall(Goal, N) :-
  findall(t, Goal, Ns),
  length(Ns, N).
  
string_number(S, N) :- number_string(N, S).

% nth0 lookup on 2D list, twice.
nnth0(Index1, Index2, List, Elem) :-
  nth0(Index1, List, SubList),
  nth0(Index2, SubList, Elem).

% firstn(?List, ?N, ?SubList, ?Rest) is det.
% List's first N items is SubList and the rest is Rest.
firstn(List, 0, [], List).
firstn([E|Es], N, [E|EsSub], Rest) :-
  N #> 0,
  Nn #= N-1,
  firstn(Es, Nn, EsSub, Rest).
% firstn(?List, ?N, ?SubList) is det.
% List's first N items is SubList.
firstn(List, N, SubList) :-
  firstn(List, N, SubList, _).


% lastn(?List, ?N, ?SubList) is det.
% List's last N items is SubList. Uses reverse/2 twice.
lastn(List, N, SubList) :-
  reverse(List, ListReversed),
  firstn(ListReversed, N, LastNReversed),
  reverse(LastNReversed, SubList).


% nil([]) --> [].
% quant(E-N) --> integer(N), blanks, prolog_var_name(E). % N is how many, E is element
% quants([Pair|Pairs]) -->  quant(Pair), ((",", blanks, quants(Pairs)) | (blanks, nil(Pairs))).
% reaction(E-(N:Ins)) --> quants(Ins), blanks, "=>", blanks, quant(E-N).
% reactions([R|RRest]) --> reaction(R), blanks, (reactions(RRest) | nil(RRest)).


% convlist(:Goal, +ListIn, -ListOut) 
% ?- convlist([X,Y]>>(integer(X), Y is X^2),
%             [3, 5, foo, 2], L).
% L = [9, 25, 4].

% maplist(:Goal, ?List1, ?List2)

% partition(:Pred, +List, ?Included, ?Excluded)
% ?- partition(>=(3), [1,2,3,4,5], L, L2).
% L = [1, 2, 3],
% L2 = [4, 5].

% prefix(?Part, ?Whole) true if Part is a leading substring of Whole

% subseq(+List, -SubList, -Complement)

% split_string("/home//jan///nice/path", "/", "/", L).
% L = ["home", "jan", "nice", "path"].
% split_string("SWI-Prolog, 7.0", ",", " ", L).
% L = ["SWI-Prolog", "7.0"].

% number_string(?Number, ?String)

% main :- 
%   read_file_to_string('./input.txt', FStr, []),
%   string_lines(FStr, [TimeLine,DistanceLine]),
%   foreach((member(Line, Lines),
%            split_string(Line, '|', ' ', Ps1),
%            nth0(1, Ps1, Ps11),
%            split_string(Ps11, ' ', ' ', Ps2)),
%           writeln(Ps2)).
  


% sublist([], _, 0).
% sublist([X|T], [X|Rest], Len) :-
%   Len2 #= Len-1,
%   sublist(T, Rest, Len2).
% sublists(Sub, List, 0, Len) :- sublist(Sub, List, Len).
% sublists(Sub, [_|Rest], At, Len) :- At2 #= At - 1, sublists(Sub, Rest, At2, Len).
% ?- sublist(S, [1,2,3,4], 3).
% S = [1, 2, 3] ;
% false.

% ?- sublists(S, [1,2,3,4], At, 3).
% S = [1, 2, 3],
% At = 0 ;
% S = [2, 3, 4],
% At = 1 ;
% false.

