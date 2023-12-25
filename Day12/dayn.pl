:- set_prolog_flag(double_quotes, chars).
:- use_module(library(dcg/basics)).
:- use_module(library(clpfd)).
:- use_module(library(yall)).
:- use_module(library(apply)).
:- use_module(library(reif)).
:- use_module(library(reif_utils)).
:- use_module(helpers).

readInput(ListConditions) :-
  read_file_to_string('input2.txt', FStr, []),
  string_lines(FStr, Lines),
  maplist([Line,[Springs,DamagedList]] >>
            (split_all(" ", Line, [SpringsStr, DamagedStr]),
            string_chars(SpringsStr, Springs),
            split_all(",", DamagedStr, DamagedListStr),
            maplist(string_number, DamagedListStr, DamagedList)),
          Lines, ListConditions).

:- set_prolog_flag(table_space, 2 000 000 000).
:- table condition(+, +, max).

%condition(Cs, Ds, N) :- writeln(condition(Cs, Ds, N)), false.
condition([], [], 1).
condition([], Ds, 0) :- Ds\=[].
condition(Cs, [], 0) :- Cs\= [].
condition([.|Cs], Ds, N) :-
  condition(Cs, Ds, N).
condition([#|Cs], [D|Ds], N) :-
  (firstn([#|Cs], D, DamagedSeg, RestCs), \+ memberchk(., DamagedSeg)) ->
    (RestCs=[] -> condition(RestCs, Ds, N) ;
      ((RestCs=[C2|RestRestCs], C2\='#') -> condition(RestRestCs, Ds, N);
        N=0)) ;
  N=0.
condition([?|Cs], Ds, N) :-
  condition([.|Cs], Ds, N1),
  condition([#|Cs], Ds, N2),
  N is N1 + N2.

partA :- 
  readInput(ListConditions),
  maplist([[Cs,Ds],Tc]>>(condition(Cs, Ds, Tc), writeln('')), ListConditions, Counts),
  writeln(Counts),
  foldl(plus, Counts, 0, Sum),
  writeln(Sum).

% Part B

partB_([Springs, DamagedList], Count) :-
  get_time(Stamp),
  stamp_date_time(Stamp, date(_,_,_,H,M,Sf,_,_,_), local),
  S is round(Sf),
  writeln([H:M:S]=Springs),
  append([Springs,"?",Springs,"?",Springs,"?",Springs,"?",Springs], Springs5),
  append([DamagedList,DamagedList,DamagedList,DamagedList,DamagedList], DamagedList5),
  condition(Springs5, DamagedList5, Count).

partB :-
  readInput(SpringsList),
  time(maplist(partB_, SpringsList, Counts)),
  writeln(Counts),
  foldl(plus, Counts, 0, Sum),
  writeln(Sum).
