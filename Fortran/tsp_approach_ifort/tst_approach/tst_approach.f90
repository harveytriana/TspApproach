    !****************************************************************************
    !
    !  PROGRAM: tst_approach
    !
    !  PURPOSE: Take part in a performance measurement study on a computer problem
    !
    !****************************************************************************
    ! Traveling Salesman Problem Exact algorithm
    ! Nodes      : 13
    ! Iterations : 479001600
    !  Nodules
    !  NODE: 1
    !  NODE: 2
    !  NODE: 3
    !  NODE: 4
    !  NODE: 5
    !  NODE: 6
    !  NODE: 7
    !  NODE: 8
    !  NODE: 9
    !  NODE: 10
    !  NODE: 11
    !  NODE: 12
    ! ... 30 s

    !  RESULT
    !  NODE: 0
    !  NODE: 7
    !  NODE: 2
    !  NODE: 3
    !  NODE: 4
    !  NODE: 12
    !  NODE: 6
    !  NODE: 8
    !  NODE: 1
    !  NODE: 11
    !  NODE: 10
    !  NODE: 5
    !  NODE: 9
    !  NODE: 0
    ! Distance     : 7293
    ! Elapsed time : 7.39 s x86 | x64 7.42 s

    program tst_approach

    implicit none

    integer, parameter :: NODES_COUNT = 13
    integer, parameter :: NODULES_COUNT = NODES_COUNT - 1

    ! FUNCTIONS
    integer :: factorial

    ! VARS
    integer :: depot
    integer :: nodes
    integer :: nodulesCount
    integer :: iterations
    integer :: percentSize
    integer :: percent
    integer :: permutation
    integer :: minDistance
    integer :: finalIndex
    integer :: fragment

    ! temporaries
    integer :: i
    integer :: j
    integer :: t
    real :: startTime, stopTime

    ! define matrix
    integer, dimension(0:NODES_COUNT-1, 0:NODES_COUNT-1) :: data
    integer, dimension(0:NODULES_COUNT-1) :: nodules
    integer, dimension(0:NODULES_COUNT-1) :: route

    print *, 'Traveling Salesman Problem Exact algorithm'

    data = reshape((/  &
        0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972,  &
        2451, 0, 1745, 1524, 831, 1240, 959, 2596, 403, 1589, 1374, 357, 579,    &
        713, 1745, 0, 355, 920, 803, 1737, 851, 1858, 262, 940, 1453, 1260,      &
        1018, 1524, 355, 0, 700, 862, 1395, 1123, 1584, 466, 1056, 1280, 987,    &
        1631, 831, 920, 700, 0, 663, 1021, 1769, 949, 796, 879, 586, 371,        &
        1374, 1240, 803, 862, 663, 0, 1681, 1551, 1765, 547, 225, 887, 999,      &
        2408, 959, 1737, 1395, 1021, 1681, 0, 2493, 678, 1724, 1891, 1114, 701,  &
        213, 2596, 851, 1123, 1769, 1551, 2493, 0, 2699, 1038, 1605, 2300, 2099, &
        2571, 403, 1858, 1584, 949, 1765, 678, 2699, 0, 1744, 1645, 653, 600,    &
        875, 1589, 262, 466, 796, 547, 1724, 1038, 1744, 0, 679, 1272, 1162,     &
        1420, 1374, 940, 1056, 879, 225, 1891, 1605, 1645, 679, 0, 1017, 1200,   &
        2145, 357, 1453, 1280, 586, 887, 1114, 2300, 653, 1272, 1017, 0, 504,    &
        1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0       &
        ! sample or 4 nodes
        ! NODES_COUNT = 4
        ! 00, 10, 35, 30, &
        ! 10, 00, 30, 15, &
        ! 35, 30, 00, 30, &
        ! 30, 15, 30, 00  &
        /), shape(data))

    depot = 0
    nodes = NODES_COUNT
    nodulesCount = NODULES_COUNT
    iterations = factorial(nodulesCount)
    percentSize = iterations / 100
    fragment = percentSize;
    percent = 0
    permutation = 1
    minDistance = 999999
    finalIndex = nodulesCount - 1
    !
    call cpu_time(startTime)

    ! initialize permutation array
    j = 0
    do i = 0, nodulesCount
        if (i /= depot) then
            nodules(j) = i
            route(j) = i
            j = j + 1
        end if
    end do

    write(*, '(A, I0)') 'Nodes      : ', nodes
    write(*, '(A, I0)') 'Iterations : ', iterations
    print *, 'Nodules'
    do i = 0, finalIndex
        write(*, '(A, I0)') " NODE: ", nodules(i)
    end do

    ! recursive calculation
    call GetRoute(0, nodulesCount)

    call cpu_time(stopTime)

    print *, 'RESULT'
    write(*, '(A, I0)') ' NODE: ', depot
    do i = 0, finalIndex
        write(*, '(A, I0)') ' NODE: ', route(i)
    end do
    write(*, '(A, I0)') ' NODE: ', depot
    write(*, '(A, I0)') 'Distance     : ', minDistance
    write(*, '(A, F12.6, A)') 'Elapsed time : ',  (stopTime - startTime), ' s'

    contains
    recursive subroutine GetRoute(start, finish)
    implicit none

    integer :: start
    integer :: finish
    integer :: s
    integer :: i

    if (start == finish - 1) then
        s = data(depot, nodules(0)) + data(nodules(finalIndex), depot)
        ! 2. route
        do i = 0, finalIndex - 1
            s = s + data(nodules(i), nodules(i + 1))
        end do
        permutation = permutation + 1
        if (minDistance > s) then
            minDistance = s
            ! preserve the minimum route
            do i = 0, finalIndex
                route(i) = nodules(i)
            end do
        end if
        ! show advance
        if (percentSize > 0) then
            ! if (mod(permutation, percentSize) == 0) then
            if (permutation > fragment) then
                percent = percent + 1
                fragment = fragment + percentSize
                write(*, '(A, I0, A)')  " permutations: ", percent, " %"
            end if
        end if
    else
        do i = start, finish - 1
            ! swap
            call swap(nodules(start), nodules(i))
            ! permute
            call GetRoute(start + 1, finish)
            ! swap
            call swap(nodules(start), nodules(i))
        end do
    end if
    end subroutine

    subroutine swap(a, b)
    implicit none
    integer :: a, b
    t = a
    a = b
    b = t
    end subroutine
    end program

    function factorial(number)
    integer :: factorial
    integer :: number, f
    f = 1
    do i = 1,number
        f = f * i
    end do
    factorial = f
    end function

